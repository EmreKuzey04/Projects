using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WEB.MVCUI.Areas.Admin.Models.Entities;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            string sessionData = HttpContext.Session.GetString("LoggedInUser");

            if (sessionData == null)
            {
                return RedirectPermanent("/admin/authentication/login");
            }

            var LoggedInUser = JsonSerializer.Deserialize<User>(sessionData);

            return View(LoggedInUser);
            
        }
    }
}
