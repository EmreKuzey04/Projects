using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Helpers;

namespace WEB.MVCUI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            return View(cart);
        }

        [HttpPost]
        public IActionResult Checkout(string fullName, string address)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            if (cart == null || !cart.Any())
                return RedirectToAction("Checkout","Order");

            // TODO: Siparişi veritabanına kaydet

            // Sepeti temizle
            HttpContext.Session.Remove("cart");

            TempData["Message"] = "Siparişiniz başarıyla alındı!";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Confirmation()
        {
            return View();
        }

    }
}
