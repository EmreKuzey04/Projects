using Microsoft.AspNetCore.Mvc;

namespace WEB.MVCUI.Controllers
{
    public class ErrorPages : Controller
    {
        public IActionResult EmailCannotBeApproved()
        {
            return View();
        }
    }
}
