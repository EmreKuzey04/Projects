namespace WEB.MVCUI.Areas.Admin.Models.Dtos
{
    public class RegisterAppUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}
