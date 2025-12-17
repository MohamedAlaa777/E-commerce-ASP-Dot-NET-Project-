using BL.Contracts;
using ECommerce.BL.Contracts;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItem _item;
        private readonly IItemImage _image;
        public ItemsController(IItem item ,IItemImage itemImage)
        {
            _item = item;
            _image = itemImage;
        }
        public IActionResult ItemDetails(int id)
        {
            var item = _item.GetItemId(id);

            VmItemDetails vm = new VmItemDetails();
            vm.Item = item;
            vm.lstRecommendedItems = _item.GetRecommendedItems(id).Take(20).ToList();
            vm.lstItemImages = _image.GetByItemId(id);
            return View(vm);
        }
        public IActionResult ItemList()
        {
            return View();
        }
    }
}
