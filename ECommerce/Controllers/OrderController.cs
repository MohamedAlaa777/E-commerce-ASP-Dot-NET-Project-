using BL.Contracts;
using BL.Data;
using ECommerce.BL.Contracts;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IItem _itemService;
        private readonly ISalesInvoice _salesInvoiceService;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(IItem item , ISalesInvoice salesInvoice, UserManager<ApplicationUser> userManager)
        {
            _itemService = item;
            _salesInvoiceService = salesInvoice;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult UpdateItemQuantity([FromBody] UpdateCartItemDto dto)
        {
            if (dto == null || dto.ItemId <= 0)
                return BadRequest("Invalid input");

            var cartJson = Request.Cookies["Cart"];
            if (string.IsNullOrEmpty(cartJson))
                return NotFound("Cart is empty");

            var cart = JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
            if (cart == null || cart.lstItems == null)
                return BadRequest("Invalid cart data");

            var item = cart.lstItems.FirstOrDefault(i => i.ItemId == dto.ItemId);
            if (item == null)
                return NotFound("Item not found");

            //  qty = 0 → remove item
            if (dto.Qty <= 0)
            {
                cart.lstItems.Remove(item);
            }
            else
            {
                item.Qty = dto.Qty;
                item.Total = item.Qty * item.Price;
            }

            cart.Total = cart.lstItems.Sum(i => i.Total);

            // Update cookie
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            return Ok(new
            {
                success = true,
                itemTotal = item?.Total ?? 0,
                cartTotal = cart.Total
            });
        }

        [HttpPost]
        public IActionResult RemoveItem([FromBody] RemoveCartItemDto dto)
        {
            if (dto == null || dto.ItemId <= 0)
                return BadRequest();

            var cartJson = Request.Cookies["Cart"];
            if (string.IsNullOrEmpty(cartJson))
                return NotFound();

            var cart = JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
            if (cart == null || cart.lstItems == null)
                return BadRequest();

            var item = cart.lstItems.FirstOrDefault(i => i.ItemId == dto.ItemId);
            if (item == null)
                return NotFound();

            cart.lstItems.Remove(item);

            //  cart empty → delete cookie + tell client to redirect
            if (!cart.lstItems.Any())
            {
                Response.Cookies.Delete("Cart");

                return Ok(new
                {
                    redirect = true,
                    redirectUrl = Url.Action("Index", "Home")
                });
            }

            cart.Total = cart.lstItems.Sum(i => i.Total);

            Response.Cookies.Append("Cart",JsonConvert.SerializeObject(cart));

            return Ok(new
            {
                redirect = false,
                cartTotal = cart.Total
            });
        }



        public IActionResult Cart()
        {
            string sesstionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            return View(cart);
        }

        public IActionResult AddToCart(int itemId)
        {
            ShoppingCart cart;

            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
            else
                cart = new ShoppingCart();

            var item = _itemService.GetById(itemId);

            //copy by reference between (itemInList , cart.lstItems.Where(a => a.ItemId == itemId).FirstOrDefault();)

            var itemInList = cart.lstItems.Where(a => a.ItemId == itemId).FirstOrDefault();

            if (itemInList != null)
            {
                itemInList.Qty++;
                itemInList.Total = itemInList.Qty * itemInList.Price;
            }
            else
            {
                cart.lstItems.Add(new ShoppingCartItem
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Price = item.SalesPrice,
                    Qty = 1,
                    Total = item.SalesPrice
                });
            }
            cart.Total = cart.lstItems.Sum(a => a.Total);

            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var invoice= _salesInvoiceService.GetbyUserId(Guid.Parse(user.Id)).AsEnumerable();
            return View(invoice);
        }

        [Authorize]
        public async Task<IActionResult> OrderSuccess()
        {
            string sesstionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            await SaveOrder(cart);

            // ✅ Delete the Cart cookie
            Response.Cookies.Delete("Cart");

            return View();
        }

        async Task SaveOrder(ShoppingCart oShopingCart)
        {
            try
            {
                List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();
                foreach (var item in oShopingCart.lstItems)
                {
                    lstInvoiceItems.Add(new TbSalesInvoiceItem()
                    {
                        ItemId = item.ItemId,
                        Qty = item.Qty,
                        InvoicePrice = item.Price
                    });
                }

                var user = await _userManager.GetUserAsync(User);

                TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(5),
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now
                };

                _salesInvoiceService.Save(oSalesInvoice, lstInvoiceItems, true);
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
    }
}

