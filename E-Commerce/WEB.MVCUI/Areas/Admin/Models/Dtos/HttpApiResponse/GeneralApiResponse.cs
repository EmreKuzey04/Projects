

namespace WEB.MVCUI.Areas.Admin.Models.Dtos.HttpApiResponse
{
    public class GeneralApiResponse<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
