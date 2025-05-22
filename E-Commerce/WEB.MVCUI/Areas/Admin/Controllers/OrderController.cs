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

            ViewBag.Employees = ctx.Employees.ToList();
            ViewBag.Customers = ctx.Customers.ToList();
            ViewBag.Shippers = ctx.Shippers.ToList();

            return View();
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

        public ActionResult Edit(int id)
        {
            using var ctx = new TradewndContext();

            var order = ctx.Orders.FirstOrDefault(o => o.OrderID == id);

            if (order == null)
                return NotFound();

            ViewBag.Customers = ctx.Customers.ToList();
            ViewBag.Employees = ctx.Employees.ToList();
            ViewBag.Shippers = ctx.Shippers.ToList();

            return View(order);
        }

        
        [HttpPost]
        public ActionResult Edit(Order OrderUpdateDto)
        {
            using var ctx = new TradewndContext();
            var order = ctx.Orders.FirstOrDefault(o => o.OrderID == OrderUpdateDto.OrderID);
            if (order == null)
                return NotFound();

            order.CustomerID = OrderUpdateDto.CustomerID;
            order.EmployeeID = OrderUpdateDto.EmployeeID;
            order.ShipperID = OrderUpdateDto.ShipperID;
            order.OrderDate = OrderUpdateDto.OrderDate;
            order.Description = OrderUpdateDto.Description;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            using var ctx = new TradewndContext();
            var order = ctx.Orders.FirstOrDefault(o => o.OrderID == id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using var ctx = new TradewndContext();
            var order = ctx.Orders.FirstOrDefault(o => o.OrderID == id);
            if (order == null)
                return NotFound();

            ctx.Orders.Remove(order);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        
        }
    } 
}
