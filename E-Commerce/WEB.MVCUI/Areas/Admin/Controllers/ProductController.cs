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
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public ViewResult Index()
        {
            using var ctx = new TradewndContext();
            var products =
                ctx.Products
                .Include("Category")
                .Include("Supplier")
                .Include("Photos")
                .ToList();

            return View(products);
        }

        public ViewResult Add()
        {
            using var ctx = new TradewndContext();
            var categories = ctx.Categories.ToList();
            return View(categories);

        }

        [HttpPost]
        public IActionResult Save(ProductAddDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                UnitPrice = dto.UnitPrice,
                UnitsInStock = dto.UnitsInStock,
                CategoryId = dto.CategoryId,
                SupplierID = dto.SupplierID,
                Description = dto.Description,
                Photos = new List<ProductPhoto>()
            };

            using var ctx = new TradewndContext();
            ctx.Products.Add(product);

            string rootPath = _hostingEnvironment.WebRootPath;

            if (dto.Photos != null && dto.Photos.Any())
            {
                foreach (var item in dto.Photos)
                {
                    string extension = Path.GetExtension(item.FileName);
                    string fileName = Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                    string fullPath = Path.Combine(rootPath, "admin", "images", "productImages", fileName);

                    using FileStream fs = new FileStream(fullPath, FileMode.Create);
                    item.CopyTo(fs);

                    product.Photos.Add(new ProductPhoto
                    {
                        PhotoPath = $"/admin/images/productImages/{fileName}"
                    });
                }
            }

            ctx.SaveChanges();
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            using var ctx = new TradewndContext();
            var productToDelete = ctx.Products.SingleOrDefault(x => x.ProductId == id);

            return View(productToDelete);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            using var ctx = new TradewndContext();

           
            var productToDelete = ctx.Products.Include(p => p.Photos)
                                              .SingleOrDefault(p => p.ProductId == id);

            if (productToDelete != null)
            {
                
                if (productToDelete.Photos != null && productToDelete.Photos.Any())
                {
                    foreach (var photo in productToDelete.Photos)
                    {
                        var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, photo.PhotoPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                  
                    ctx.ProductPhotos.RemoveRange(productToDelete.Photos);
                }

            
                ctx.Products.Remove(productToDelete);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            using var ctx = new TradewndContext();
            var productToEdit = ctx.Products
                .Include(p => p.Photos) 
                .SingleOrDefault(x => x.ProductId == id);

            return View(productToEdit);
        }

        [HttpPost]
        public IActionResult Edit(ProductUpdateDto dto)
        {
            using var ctx = new TradewndContext();
            var productToEdit = ctx.Products
                .Include(p => p.Photos)
                .SingleOrDefault(x => x.ProductId == dto.ProductId);

            if (productToEdit == null)
                return NotFound();

            
            productToEdit.ProductName = dto.ProductName;
            productToEdit.UnitPrice = dto.UnitPrice;
            productToEdit.UnitsInStock = dto.UnitsInStock;
            productToEdit.Description = dto.Description;
            productToEdit.CategoryId = dto.CategoryId;
            productToEdit.SupplierID = dto.SupplierID;

            
            if (dto.Photos != null && dto.Photos.Any())
            {
                
                if (productToEdit.Photos != null && productToEdit.Photos.Any())
                {
                    foreach (var photo in productToEdit.Photos)
                    {
                        var existingPhotoPath = Path.Combine(_hostingEnvironment.WebRootPath, photo.PhotoPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                        if (System.IO.File.Exists(existingPhotoPath))
                        {
                            System.IO.File.Delete(existingPhotoPath);
                        }
                    }
                    ctx.ProductPhotos.RemoveRange(productToEdit.Photos);
                }

                
                string rootPath = _hostingEnvironment.WebRootPath;
                string uploadPath = Path.Combine(rootPath, "Admin", "productImages");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var newPhotos = new List<ProductPhoto>();

                foreach (var photoFile in dto.Photos)
                {
                    if (photoFile != null && photoFile.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                        string filePath = Path.Combine(uploadPath, fileName);

                        using (var fs = new FileStream(filePath, FileMode.Create))
                        {
                            photoFile.CopyTo(fs);
                        }

                        newPhotos.Add(new ProductPhoto
                        {
                            ProductId = productToEdit.ProductId,
                            PhotoPath = $"/admin/productImages/{fileName}"
                        });
                    }
                }

                ctx.ProductPhotos.AddRange(newPhotos);
            }

            ctx.Products.Update(productToEdit);
            ctx.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
