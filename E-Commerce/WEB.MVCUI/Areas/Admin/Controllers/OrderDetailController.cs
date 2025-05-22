using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrderDetailController : Controller
    {
        public ViewResult Index()
        {
            using var ctx = new TradewndContext();
            var orderdetail = ctx.OrderDetails
                .Include("Order")
                .Include("Product")
                .ToList();

            return View(orderdetail);
        }

        public ViewResult Add()
        {
            using var ctx = new TradewndContext();
            var product = ctx.Products.ToList();
            return View(product);
        }

        public ViewResult Save(OrderDetailAddDto dto)
        {
            var orderDetail = new OrderDetail();

            orderDetail.OrderDetailID = dto.OrderDetailID;
            orderDetail.OrderID = dto.OrderID;
            orderDetail.ProductId = dto.ProductID;
            orderDetail.Quantity = dto.Quantity;

            using var ctx = new TradewndContext();
            ctx.OrderDetails.Add(orderDetail);
            ctx.SaveChanges();
            return View();
        }

      
        public IActionResult Edit(int id) 
        {
            using var ctx = new TradewndContext();
            var orderDetail = ctx.OrderDetails.FirstOrDefault(od => od.OrderDetailID == id);

            if (orderDetail == null)
                return NotFound();

           
            ViewBag.Products = ctx.Products.ToList();

            var dto = new OrderDetailUpdateDto
            {
                OrderDetailID = orderDetail.OrderDetailID,
                OrderID = orderDetail.OrderID,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity
            };

            return View(dto);
        }

        
        [HttpPost]
        public IActionResult Edit(OrderDetailUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                using var ctx = new TradewndContext();
                ViewBag.Products = ctx.Products.ToList();
                return View(model);
            }

            using var ctx2 = new TradewndContext();
            var orderDetail = ctx2.OrderDetails.FirstOrDefault(od => od.OrderDetailID == model.OrderDetailID);
            if (orderDetail == null)
                return NotFound();

            orderDetail.ProductId = model.ProductId;
            orderDetail.Quantity = model.Quantity;

            ctx2.SaveChanges();

            
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            using var ctx = new TradewndContext();
            var orderDetail = ctx.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                .FirstOrDefault(od => od.OrderDetailID == id);

            if (orderDetail == null)
                return NotFound();

            return View(orderDetail);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            using var ctx = new TradewndContext();
            var orderDetail = ctx.OrderDetails.FirstOrDefault(od => od.OrderDetailID == id);

            if (orderDetail == null)
                return NotFound();

            ctx.OrderDetails.Remove(orderDetail);
            ctx.SaveChanges();

          
            return RedirectToAction("Index");
        }
    }
}
