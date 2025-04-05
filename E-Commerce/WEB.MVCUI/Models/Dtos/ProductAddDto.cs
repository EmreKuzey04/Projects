using WEB.MVCUI.Models.Entities;

namespace WEB.MVCUI.Models.Dtos
{
    public class ProductAddDto
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public string? Photo { get; set; }
        public int SupplierID { get; set; }
        public string? Description { get; set; }


    }
}
