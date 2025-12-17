using ECommerce.BL.Contracts;
using ECommerce.BL.Services;
using ECommerce.Models;
using LapShop.Utlities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin,Data Entry")]
    public class ItemsController : Controller
    {
        private readonly IItem _items;
        private readonly ICategory _category;
        private readonly IOs _os;
        private readonly IItemType _itemType;
        public ItemsController(IItem item, ICategory category, IOs os, IItemType itemType )
        {
            _items = item;
            _category = category;
            _os = os;
            _itemType = itemType;
        }
        public IActionResult List()
        {
            ViewBag.lstCategories = _category.GetAll();
            var items = _items.GetAllItemsData(null).AsQueryable();
            return View(items);
        }

        public IActionResult Search(int id) 
        {
            ViewBag.lstCategories = _category.GetAll();
            var items = _items.GetAllItemsData(id);
            return View("List", items);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? itemId)
        {
            var item = new TbItem();
            ViewBag.lstCategories= _category.GetAll();
            ViewBag.lstItemTypes = _itemType.GetAll();
            ViewBag.lstOs = _os.GetAll();
            if (itemId != null)
                item = _items.GetById((int)itemId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", item);

            item.ImageName = await Helper.UploadImage(Files, "Items");

            _items.Save(item);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int itemId)
        {
            _items.Delete(itemId);
            return RedirectToAction("List");
        }
    }
}
