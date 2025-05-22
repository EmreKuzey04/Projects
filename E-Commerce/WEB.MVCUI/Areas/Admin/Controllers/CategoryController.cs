using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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

        public IActionResult Index()
        {
            using var ctx = new TradewndContext();
            var categories = ctx.Categories.ToList();
            return View(categories);
        }

        public IActionResult Add()
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
            string uploadPath = Path.Combine(rootPath, "Admin","images" ,"categoryImages");

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
                Photo = $"/admin/images/categoryImages/{fileName}" // Veritabanına kaydedilecek URL
            };

            using var ctx = new TradewndContext();
            ctx.Categories.Add(category);
            ctx.SaveChanges();

            return View();
        }

        public IActionResult Delete(int id)
        {
            using var ctx = new TradewndContext();
            var categoryToDelete= ctx.Categories.SingleOrDefault(x=> x.CategoryID == id);

            return View(categoryToDelete);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            using var ctx = new TradewndContext();
            var categoryToDelete = ctx.Categories.SingleOrDefault(x => x.CategoryID == id);
            ctx.Categories.Remove(categoryToDelete);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            using var ctx = new TradewndContext();
            var categoryToEdit = ctx.Categories.SingleOrDefault(x => x.CategoryID == id);

            return View(categoryToEdit);
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryUpdateDto dto)
        {
            using var ctx = new TradewndContext();
            var categoryToEdit = ctx.Categories.SingleOrDefault(x => x.CategoryID == dto.CategoryID);

            categoryToEdit.CategoryName = dto.CategoryName;
            categoryToEdit.Description = dto.Description;
            
            if (dto.Photo != null)
            {
                // önceki kategorinin resmini silme
                System.IO.File.Delete($"{_hostingEnvironment.WebRootPath}/{categoryToEdit.Photo}");
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


                categoryToEdit.Photo = $"/admin/categoryImages/{fileName}"; // Veritabanına kaydedilecek URL
                


            }
            ctx.Categories.Update(categoryToEdit);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteSelected(List<int> ids)
        {
            using var ctx = new TradewndContext();
            var categoriesToDelete = ctx.Categories.Where(x=> ids.Contains(x.CategoryID)).ToList();
            ctx.Categories.RemoveRange(categoriesToDelete);
            ctx.SaveChanges();

            return Json(new { IsOk= true});
        }
    }
}
  
