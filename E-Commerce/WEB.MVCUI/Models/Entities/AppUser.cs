namespace WEB.MVCUI.Models.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailApproved { get; set; }
        public bool IsActive { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string EmailActivationCode { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public City City { get; set; }
        public District District { get; set; }
    }
}
