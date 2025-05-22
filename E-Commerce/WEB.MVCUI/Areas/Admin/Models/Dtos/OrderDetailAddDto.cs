namespace WEB.MVCUI.Areas.Admin.Models.Dtos
{
    public class OrderDetailAddDto
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
      
    }
}
