using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ShipperController : Controller
    {
        public ViewResult Index()
        {
            using var ctx = new TradewndContext();
            var shippers = ctx.Shippers.ToList();
            return View(shippers);
        }

        public ViewResult Add()
        {
            return View();
        }

        public ViewResult Save(ShipperAddDto dto)
        {
            var shipper = new Shipper();
            shipper.ShipperName = dto.ShipperName;
            shipper.Phone = dto.Phone;
            using var ctx = new TradewndContext();
            ctx.Shippers.Add(shipper);

            ctx.SaveChanges();
            return View();
        }
    }
}
