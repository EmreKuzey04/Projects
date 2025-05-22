namespace WEB.MVCUI.Areas.Admin.Models.Entities
{
    public class District
    {
        public int Id { get; set; }
        public string DistrictName { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public List<AppUser> AppUsers { get; set; }
    }
}
