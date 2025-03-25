using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;


     namespace WEB.MVCUI.Areas.Admin.Controllers
    { 


        [Area("admin")]
        public class CategoryController : Controller
        {
            private readonly IWebHostEnvironment _hostingEnvironment;

            public CategoryController(IWebHostEnvironment hostingEnvironment)
            {
                _hostingEnvironment = hostingEnvironment;
            }

            public ViewResult Index()
            {
                using var ctx = new TradewndContext();
                var categories = ctx.Categories.ToList();
                return View(categories);
            }

            public ViewResult Add()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Save(CategoryAddDto dto)
            {
                if (dto.Photo == null || dto.Photo.Length == 0)
                {
                    ModelState.AddModelError("Photo", "Lütfen bir resim seçin.");
                    return View();
                }

                // wwwroot yolunu al
                string rootPath = _hostingEnvironment.WebRootPath;

                // Kayıt dizinini belirle
                string uploadPath = Path.Combine(rootPath, "Admin", "categoryImages");

                // Eğer klasör yoksa oluştur
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Benzersiz bir dosya adı oluştur (Önlemek için)
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Photo.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                // Dosyayı kaydet
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    dto.Photo.CopyTo(fs);
                }

                // Kategori nesnesini oluştur ve veritabanına ekle
                var category = new Category
                {
                    CategoryName = dto.CategoryName,
                    Description = dto.Description,
                    Photo = $"/admin/categoryImages/{fileName}" // Veritabanına kaydedilecek URL
                };

                using var ctx = new TradewndContext();
                ctx.Categories.Add(category);
                ctx.SaveChanges();

                return View();
            }
        }
     }
