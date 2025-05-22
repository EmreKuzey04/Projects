namespace WEB.MVCUI.Areas.Admin.Models.Dtos
{
    public class OrderDetailUpdateDto
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
