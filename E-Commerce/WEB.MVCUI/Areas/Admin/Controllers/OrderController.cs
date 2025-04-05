using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrderController : Controller
    {
        public ViewResult Index()
        {
            using var ctx = new TradewndContext();
            var orders =
                ctx.Orders.Include("Customer")
                          .Include("Employee")
                          .Include("Shipper")

                .ToList();

            return View(orders);
        }

        public ViewResult Add()
        {
            using var ctx = new TradewndContext();
            var employee = ctx.Employees.ToList();
            return View(employee);
        }

        public ViewResult Save(OrderAddDto dto)
        {
            var order = new Order();
            
            order.CustomerID = dto.CustomerID;
            order.EmployeeID = dto.EmployeeID;
            order.ShipperID = dto.ShipperID;
            order.OrderDate = dto.OrderDate;
            order.Description = dto.Description;
            using var ctx = new TradewndContext();
            ctx.Orders.Add(order);

            ctx.SaveChanges();
            return View();
        }
    }
}
