using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SupplierController : Controller
    {
        public ViewResult Index()
        {
            using var ctx = new TradewndContext();
            var supplier =
                ctx.Suppliers.ToList();

            return View(supplier);
        }

        public ViewResult Add()
        {
            return View();
        }

        public ViewResult Save(SupplierAddDto dto)
        {
            var supplier  = new Supplier();
            supplier.SupplierID = dto.SupplierID;
            supplier.SupplierName = dto.SupplierName;
            supplier.ContactName = dto.ContactName;
            supplier.Address = dto.Address;
            supplier.City = dto.City;
            supplier.Country = dto.Country;
            supplier.PostalCode = dto.PostalCode;
            supplier.Phone = dto.Phone;
            using var ctx = new TradewndContext();
            ctx.Suppliers.Add(supplier);

            ctx.SaveChanges();
            return View();
        }
    }
}
