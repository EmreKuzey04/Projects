namespace WEB.MVCUI.Models.ViewModels
{
    public class MyOrderViewModel
    {
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Description { get; set; }
        public decimal? Total { get; set; }
        public List<MyOrderProductViewModel> Products { get; set; } = new();
    }
    public class MyOrderProductViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
