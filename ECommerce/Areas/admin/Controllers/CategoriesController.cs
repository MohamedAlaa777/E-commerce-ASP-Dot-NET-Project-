using Microsoft.AspNetCore.Mvc;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using ECommerce.BL.Contracts;
using LapShop.Utlities;

namespace ECommerce.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategory _categories;
        public CategoriesController(ICategory category)
        {
            _categories = category;
        }
        public IActionResult List()
        {
            return View(_categories.GetAll());
        }
                                  //this parameter must have the same of id of category
        public IActionResult Edit(int? categoryId)
        {
            var category = new TbCategory();
            if (categoryId != null) 
            {
                category = _categories.GetById(Convert.ToInt32(categoryId.Value));
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category ,List<IFormFile> Files)
        {
            if(!ModelState.IsValid)
                return View("Edit",category);

            category.ImageName = await Helper.UploadImage(Files, "Categories");
            _categories.Save(category);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int categoryId) 
        {
            _categories.Delete(categoryId);
            return RedirectToAction("List");
        }

        
    }
}
