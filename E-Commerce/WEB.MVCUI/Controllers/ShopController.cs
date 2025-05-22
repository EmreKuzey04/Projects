using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.MVCUI.Areas.Admin.Models.Entities;
using WEB.MVCUI.Areas.Admin.Models.Contexts;

namespace WEB.MVCUI.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            using var ctx = new TradewndContext();
            var products = ctx.Products
                              .Include("Photos")
                              .ToList();

            return View(products);
        }
    }
}
