

namespace WEB.MVCUI.Areas.Admin.Models.Dtos.HttpApiResponse
{
    public class ApiAuthData
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public DateTime expiration { get; set; }
    }
}
