namespace WEB.MVCUI.Models.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public List<District> Districts { get; set; }

        public List<AppUser> AppUsers { get; set; }
    }
}
