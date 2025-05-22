namespace WEB.MVCUI.Areas.Admin.Models.Dtos
{
    public class CategoryUpdateDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public string Description { get; set; }
        public IFormFile Photo { get; set; }

    }
}
