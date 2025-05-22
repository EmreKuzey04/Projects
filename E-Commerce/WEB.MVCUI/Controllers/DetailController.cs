using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.MVCUI.Areas.Admin.Models.Entities;
using WEB.MVCUI.Areas.Admin.Models.Contexts;

namespace WEB.MVCUI.Controllers
{
    public class DetailController : Controller
    {
   
        public IActionResult Index(int id)
        {
            using var ctx = new TradewndContext();
            var product = ctx.Products
                .Include("Photos")
                .Where(x => x.ProductId == id)
                .SingleOrDefault();
           

            if (product == null)
            {
                return NotFound();
            }
          
            return View(product);
        }


    }
}
