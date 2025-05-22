using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.MVCUI.Areas.Admin.Models.Contexts;

namespace WEB.MVCUI.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var ctx = new TradewndContext();
            var products = ctx.Products
                              .Include("Photos")
                              .ToList();

            return View(products);


        }
    }
}
