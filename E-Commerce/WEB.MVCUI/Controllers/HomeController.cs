using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ng.Services;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Entities;


namespace WEB.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Ip()
        {
            
            var userAgentService = new UserAgentService();
            var userAgent = userAgentService.Parse(Request.Headers["User-Agent"].ToString());

            var remoteIp = HttpContext.Connection.RemoteIpAddress;
            var localIp = HttpContext.Connection.LocalIpAddress;


            return View();

            //if(userAgent.IsMobile)

        }


        public IActionResult Index()
        { 
           using var ctx = new TradewndContext();
            var categories = ctx.Categories
                .Include("Products")
                .ToList();
            return View(categories);
        
        
        }




        public IActionResult ListCategory()
        {
            using var ctx = new TradewndContext();
            var categories = ctx.Categories.ToList();
            return View(categories);
        }
    }
}
