namespace WEB.MVCUI.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public short? UnitsInStock { get; set; }
        public decimal? UnitPrice { get; set; }
        public int SupplierID { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }

        public Category? Category { get; set; }
        public Supplier Supplier { get; set; }

    }
}
