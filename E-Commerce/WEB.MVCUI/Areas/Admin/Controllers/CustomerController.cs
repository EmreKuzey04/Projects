using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CustomerController : Controller
    {
            public ViewResult Index()
        {
            using var ctx = new TradewndContext();
            var customers =
                ctx.Customers.ToList();

            return View(customers);
        }

        public ViewResult Add()
        {
            return View();
        }

        public ViewResult Save(CustomerAddDto dto)
        {
             var customer = new Customer();
            customer.CustomerName = dto.CustomerName;
            customer.ContactName = dto.ContactName;
            customer.Address = dto.Address;
            customer.City = dto.City;
            customer.Country = dto.Country;
            customer.PostalCode = dto.PostalCode;
            using var ctx = new TradewndContext();
            ctx.Customers.Add(customer);

            ctx.SaveChanges();
            return View();
        }
        public IActionResult Edit(int id)
        {
            using var ctx = new TradewndContext();
            var customer = ctx.Customers.SingleOrDefault(c => c.CustomerID == id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Admin/Customer/EditCustomer
        [HttpPost]
        public IActionResult EditCustomer(int customerId, CustomerUpdateDto dto)
        {
            using var ctx = new TradewndContext();
            var customer = ctx.Customers.SingleOrDefault(c => c.CustomerID == customerId);
            if (customer == null) return NotFound();

            customer.CustomerName = dto.CustomerName;
            customer.ContactName = dto.ContactName;
            customer.Address = dto.Address;
            customer.City = dto.City;
            customer.PostalCode = dto.PostalCode;
            customer.Country = dto.Country;

            ctx.Customers.Update(customer);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            using var ctx = new TradewndContext();
            var customer = ctx.Customers.SingleOrDefault(c => c.CustomerID == id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Admin/Customer/DeleteConfirmed
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            using var ctx = new TradewndContext();
            var customer = ctx.Customers.SingleOrDefault(c => c.CustomerID == id);
            if (customer == null) return NotFound();

            ctx.Customers.Remove(customer);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
        

