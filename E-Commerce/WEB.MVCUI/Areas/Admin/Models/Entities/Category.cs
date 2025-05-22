namespace WEB.MVCUI.Areas.Admin.Models.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
        public string Photo { get; set; }

        public List<Product>? Products { get; set; }


    }
}
