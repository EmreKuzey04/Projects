namespace Application.Models.Dtos
{
    public class ProductGetDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }


    }
}
