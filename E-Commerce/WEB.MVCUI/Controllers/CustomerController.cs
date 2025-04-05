using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Models.Contexts;
using WEB.MVCUI.Models.Dtos;
using WEB.MVCUI.Models.Entities;

namespace WEB.MVCUI.Controllers
{
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
    }
        
}
