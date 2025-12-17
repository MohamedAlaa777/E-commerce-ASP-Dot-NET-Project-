using BL.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class PagesController : Controller
    {
       private readonly IPage _Page;
        public PagesController(IPage page)
        {
            _Page = page;
        }
        // GET: PagesController
        public ActionResult Index(int pageId)
        {
            var page = _Page.GetById(pageId);
            return View(page);
        }
    }
}
