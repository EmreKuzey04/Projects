using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.MVCUI.Models.Contexts;
using WEB.MVCUI.Models.Dtos;
using WEB.MVCUI.Models.Entities;

namespace WEB.MVCUI.Controllers
{
    public class OrderDetailController : Controller
    {
        public ViewResult Index()
        {
            using var ctx = new TradewndContext();
            var orderdetail =
                ctx.OrderDetails.Include("Order")
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
            var OrderDetail = new OrderDetail();

            OrderDetail.OrderDetailID = dto.OrderDetailID;
            OrderDetail.OrderID = dto.OrderID;
            OrderDetail.ProductID = dto.ProductID;
            OrderDetail.Quantity = dto.Quantity;
            using var ctx = new TradewndContext();
            ctx.OrderDetails.Add(OrderDetail);

            ctx.SaveChanges();
            return View();
        }
    }
}
