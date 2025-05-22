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
        public IActionResult Edit(int id)
        {
            using var ctx = new TradewndContext();
            var supplier = ctx.Suppliers.FirstOrDefault(s => s.SupplierID == id);

            if (supplier == null)
                return NotFound();

            var dto = new SupplierUpdateDto
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.SupplierName,
                ContactName = supplier.ContactName,
                Address = supplier.Address,
                City = supplier.City,
                PostalCode = supplier.PostalCode,
                Country = supplier.Country,
                Phone = supplier.Phone
            };

            return View(dto);
        }

        // POST: admin/Supplier/Edit
        [HttpPost]
        public IActionResult Edit(SupplierUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using var ctx = new TradewndContext();
            var supplier = ctx.Suppliers.FirstOrDefault(s => s.SupplierID == model.SupplierID);

            if (supplier == null)
                return NotFound();

            supplier.SupplierName = model.SupplierName;
            supplier.ContactName = model.ContactName;
            supplier.Address = model.Address;
            supplier.City = model.City;
            supplier.PostalCode = model.PostalCode;
            supplier.Country = model.Country;
            supplier.Phone = model.Phone;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            using var ctx = new TradewndContext();
            var supplier = ctx.Suppliers.FirstOrDefault(s => s.SupplierID == id);

            if (supplier == null)
                return NotFound();

            return View(supplier); // Silme onayı için detay gösterilecekse
        }

        // POST: admin/Supplier/DeleteConfirmed
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            using var ctx = new TradewndContext();
            var supplier = ctx.Suppliers.FirstOrDefault(s => s.SupplierID == id);

            if (supplier == null)
                return NotFound();

            ctx.Suppliers.Remove(supplier);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
