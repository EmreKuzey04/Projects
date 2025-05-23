using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Helpers;
using System.Collections.Generic;
using System.Linq;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;
using WEB.MVCUI.ActionFilters;
using System;
using WEB.MVCUI.Models.ViewModels;
using System.Text.Json;

namespace WEB.MVCUI.Controllers
{
    [CheckSession]
    public class CartController : Controller
    {
        private const string CartSessionKey = "cart";

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            using var ctx = new TradewndContext();
            var product = ctx.Products
                .Where(p => p.ProductId == productId)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.UnitPrice,
                    Photos = p.Photos.Select(ph => ph.PhotoPath).ToList()
                })
                .FirstOrDefault();

            if (product == null)
                return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();

            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
                item.Quantity += quantity;
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ImageUrl = product.Photos?.FirstOrDefault() ?? "/img/default-product.jpg",
                    Price = product.UnitPrice ?? 0,
                    Quantity = quantity
                });
            }

            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey);
            if (cart != null)
            {
                var item = cart.FirstOrDefault(x => x.ProductId == productId);
                if (item != null)
                    cart.Remove(item);

                HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey);
            if (cart == null)
                return RedirectToAction("Index");

            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey);
            if (cart == null || !cart.Any())
                return RedirectToAction("Index");

            using var ctx = new TradewndContext();
            ViewBag.Employees = ctx.Employees
                .Select(e => new { e.EmployeeID, EmployeeName = e.LastName ?? e.EmployeeID.ToString() })
                .ToList();
            ViewBag.Shippers = ctx.Shippers
                .Select(s => new { s.ShipperID, ShipperName = s.ShipperName ?? s.ShipperID.ToString() })
                .ToList();

            return View(cart);
        }

        [HttpPost]
        public IActionResult Checkout(string fullName, string address, int employeeId, int shipperId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey);
            if (cart == null || !cart.Any())
                return RedirectToAction("Checkout");

            var appUserJson = HttpContext.Session.GetString("ActiveAppUser");

            if (string.IsNullOrEmpty(appUserJson))
                return RedirectToAction("Login", "AppUser");

            var appUser = JsonSerializer.Deserialize<AppUser>(appUserJson);
            var email = appUser.Email;

            using var ctx = new TradewndContext();
            var customer = ctx.Customers.FirstOrDefault(c => c.Email == email);

            if (customer == null)
            {
                customer = new Customer
                {
                    CustomerName = fullName,
                    Email = email,
                    Address = address,
                    ContactName = fullName
                };
                ctx.Customers.Add(customer);
                ctx.SaveChanges();
            }

            var order = new Order
            {
                CustomerID = customer.CustomerID,
                EmployeeID = employeeId,
                ShipperID = shipperId,
                OrderDate = DateTime.Now,
                Description = $"Sipariş oluşturuldu: {fullName}"
            };

            ctx.Orders.Add(order);
            ctx.SaveChanges();

            foreach (var item in cart)
            {
                var detail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                ctx.OrderDetails.Add(detail);
            }

            ctx.SaveChanges();
            HttpContext.Session.Remove(CartSessionKey);

            TempData["Message"] = "Siparişiniz başarıyla alındı!";
            return RedirectToAction("Confirmation");
        }


        public IActionResult Confirmation()
        {
            return View();
        }

      
        public IActionResult MyOrders()
        {
            var appUserJson = HttpContext.Session.GetString("ActiveAppUser");
            if (string.IsNullOrEmpty(appUserJson))
            {
                return RedirectToAction("Login", "AppUser");
            }

            var appUser = JsonSerializer.Deserialize<AppUser>(appUserJson);
            var email = appUser.Email;

            using var ctx = new TradewndContext();

            var customer = ctx.Customers.FirstOrDefault(c => c.Email == email);
            if (customer == null)
            {
                TempData["Message"] = "Sipariş geçmişi bulunamadı.";
                return RedirectToAction("Index");
            }

            var orders = ctx.Orders
     .Where(o => o.CustomerID == customer.CustomerID)
     .OrderByDescending(o => o.OrderDate)
     .Select(o => new MyOrderViewModel
     {
         OrderID = o.OrderID,
         OrderDate = o.OrderDate,
         Description = o.Description,
         Total = ctx.OrderDetails
             .Where(od => od.OrderID == o.OrderID)
             .Join(ctx.Products,
                 od => od.ProductId,
                 p => p.ProductId,
                 (od, p) => (p.UnitPrice ?? 0) * od.Quantity)
             .Sum(),
         Products = ctx.OrderDetails
             .Where(od => od.OrderID == o.OrderID)
             .Join(ctx.Products,
                 od => od.ProductId,
                 p => p.ProductId,
                 (od, p) => new MyOrderProductViewModel
                 {
                     ProductName = p.ProductName,
                     Quantity = od.Quantity,
                     UnitPrice = p.UnitPrice
                 }).ToList()
     }).ToList();


            return View(orders);
        }
    }
}
