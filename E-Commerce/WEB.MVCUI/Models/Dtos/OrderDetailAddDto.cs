using WEB.MVCUI.Models.Entities;

namespace WEB.MVCUI.Models.Dtos
{
    public class OrderDetailAddDto
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
      
    }
}
