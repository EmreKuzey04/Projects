using Microsoft.AspNetCore.Mvc;
using Ng.Services;
using WEB.MVCUI.Models.Contexts;

namespace WEB.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            var userAgentService = new UserAgentService();
            var userAgent = userAgentService.Parse(Request.Headers["User-Agent"].ToString());

            var remoteIp = HttpContext.Connection.RemoteIpAddress;
            var localIp = HttpContext.Connection.LocalIpAddress;


            return View();

            //if(userAgent.IsMobile)

        }




        public IActionResult ListCategory()
        {
            using var ctx = new TradewndContext();
            var categories = ctx.Categories.ToList();
            return View(categories);
        }
    }
}
