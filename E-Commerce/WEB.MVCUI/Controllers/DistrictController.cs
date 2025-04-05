using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Models.Contexts;

namespace WEB.MVCUI.Controllers
{
    public class DistrictController : Controller
    {
        [HttpPost]
        public IActionResult GetByCity(int cityId)
        {
            if (cityId == -1)
            {
                return Json(new {IsOk = false, Message="Lütfen Şehir Seçiniz"} );

            }
            using (var ctx = new TradewndContext())
            {
              var districts = ctx.Districts.Where(x=>x.CityId == cityId).ToList();
                return Json(new {IsOk = true, Districts = districts});
            }    
        }
    }
}
