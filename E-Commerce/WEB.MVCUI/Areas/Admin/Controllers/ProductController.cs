using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
            public ViewResult Index()
            {
                using var ctx = new TradewndContext();
                var products = 
                    ctx.Products
                    .Include("Category")
                    .Include("Supplier")
                    .ToList();

                return View(products);
            }

            public ViewResult Add()
            {
               using var ctx = new TradewndContext();
               var categories = ctx.Categories.ToList();
                return View(categories);

        }

        public ViewResult Save(ProductAddDto dto)
        {
            string errorMessages = string.Empty;

            if (dto.CategoryId == -1 || dto.CategoryId == 0)
            
              errorMessages += "Kategori Seçilmelidir <br/>";
            
            
 
            if (string.IsNullOrEmpty(dto.ProductName))
            
              errorMessages += "Ürün Adı Boş Bırakılamaz <br/>";
            
                

            if (dto.UnitPrice < 1 || dto.UnitPrice > 10000000)

              errorMessages += "Ürün Fiyatı 1'den Küçük ve 10000000'den Büyük Olamaz <br/>";
            

            if (string.IsNullOrEmpty(errorMessages))
            {
                using var ctx = new TradewndContext();
                var product = new Product();
                product.ProductName = dto.ProductName;
                product.UnitPrice = dto.UnitPrice;
                product.UnitsInStock = dto.UnitsInStock;
                product.CategoryId = dto.CategoryId;
                product.SupplierID = dto.SupplierID;
                product.Description = dto.Description;
                
                ctx.Products.Add(product);
                ctx.SaveChanges();

                ViewBag.ResultColor = "green";
                ViewBag.Result = "KAYIT BAŞARILI";
                ViewBag.Message = "Ürün kaydınız yapıldı.";
              
            }
            else
            {
                ViewBag.ResultColor = "red";
                ViewBag.Result = "KAYIT BAŞARISIZ";
                ViewBag.Message = errorMessages;
            }
            return View();
        }

    }
}
