namespace WEB.MVCUI.Areas.Admin.Models.Dtos
{
    public class OrderAddDto
    {
       
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public int ShipperID { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Description { get; set; }

    }
}
