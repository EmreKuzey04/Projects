namespace WEB.MVCUI.Areas.Admin.Models.Entities
{
    public class ProductPhoto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? PhotoPath { get; set; }
        public Product? Product { get; set; }
    }
}
