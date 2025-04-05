using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Models.Contexts;

namespace WEB.MVCUI.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using (var ctx = new TradewndContext())
            {
                var categories = ctx.Categories.ToList();

                return View(categories);
            }
        }
    }
}
