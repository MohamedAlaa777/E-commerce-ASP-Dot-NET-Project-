using BL.Contracts;
using ECommerce.BL.Contracts;
using ECommerce.BL.Services;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItem _item;
        private readonly ICategory _category;
        private readonly ISlider _slider;
        public HomeController(ILogger<HomeController> logger, IItem item, ICategory category, ISlider slider)
        {
            _logger = logger;
            _item = item;
            _category = category;
            _slider = slider;
        }

        public async Task<IActionResult> Index()
        {
            VmHomePage vm = new VmHomePage();
            vm.lstAllItems = await _item.GetAllItemsData(null).Take(10).ToListAsync();
            vm.lstRecommendedItems = await _item.GetAllItemsData(null).Skip(10).Take(5).ToListAsync();
            vm.lstNewItems = await _item.GetAllItemsData(null).Skip(15).Take(5).ToListAsync();
            vm.lstFreeDelivry = await _item.GetAllItemsData(null).Skip(20).Take(4).ToListAsync();
            vm.lstSliders = _slider.GetAll();
            vm.lstCategories = _category.GetAll().Take(4).ToList();
            return View(vm);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
