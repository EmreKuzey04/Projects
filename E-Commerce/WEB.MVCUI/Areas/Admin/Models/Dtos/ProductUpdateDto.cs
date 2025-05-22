namespace WEB.MVCUI.Areas.Admin.Models.Dtos
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public int SupplierID { get; set; }
        public string? Description { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
