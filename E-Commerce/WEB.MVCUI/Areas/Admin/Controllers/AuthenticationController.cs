using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text.Json;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Contexts;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AuthenticationController : Controller
    {
        
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(UserLogInDto dto)
        {
            using var ctx = new TradewndContext();
            var user = ctx.Users.SingleOrDefault(x=> x.Email == dto.Email && x.Password == dto.Password);

            if (user != null)
            {
                if (user.Status.HasValue && !user.Status.Value )
                {
                    ViewBag.Message = "<div class= 'alert alert-danger'>Bu Kullanıcı Aktif Değil. Lütfen Sistem Yöneticisiyle İletişime Geçin.</div>";
                    return View();
                }
                else
                {
                    string userJsonData = JsonSerializer.Serialize(user);

                    HttpContext.Session.SetString("LoggedInUser",userJsonData);
                    return RedirectPermanent("/admin/home/index");
                }


            }
            else
            {
                ViewBag.Message = "<div class= 'alert alert-danger'>Böyle Bir Kullanıcı Bulunamadı</div>";
                return View();
            }

        }
    }
}
