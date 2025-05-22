

namespace WEB.MVCUI.Areas.Admin.HttpApiServices
{
    public interface IHttpApiService
    {
        Task<T> GetData<T>(string endpoint, string token = null);
        Task<T> PostData<T>(string endpoint, string jsonData, string token = null);
        Task<T> DeleteData<T>(string endpoint, string token = null);
    }
}
