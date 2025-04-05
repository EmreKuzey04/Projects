using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Models.Contexts;

namespace WEB.MVCUI.Controllers
{
    public class DetailController : Controller
    {
        public IActionResult Index()
        {
            using var ctx = new TradewndContext();
            var categories = ctx.Categories.ToList();
            return View(categories);
        }
    }
}
