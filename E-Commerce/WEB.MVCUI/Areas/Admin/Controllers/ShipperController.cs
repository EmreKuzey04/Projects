using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WEB.MVCUI.Areas.Admin.HttpApiServices;
using WEB.MVCUI.Areas.Admin.Models.ApiResponseObj;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Dtos.HttpApiResponse;
using WEB.MVCUI.Areas.Admin.Models.Entities;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ShipperController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public ShipperController(IHttpApiService httpApiService)
        {
            _httpApiService=httpApiService;
        }
        public async Task<IActionResult> Index()
        {
            var authObj = new ApiAuthGetTokenRequestObject
            {
                Email = "mvcuser1@gmail.com",
                Password = "mvcuser123"
            };

            var response = await _httpApiService.PostData<GeneralApiResponse<ApiAuthData>>(
                "Auth/getaccesstoken",
                JsonSerializer.Serialize(authObj)
            );

            if (response == null || response.Data == null || string.IsNullOrEmpty(response.Data.accessToken))
            {
                ViewBag.Error = "Token alınamadı. Lütfen kimlik doğrulama servisini kontrol edin.";
                return View(new List<ApiShipperResponseObj>());
            }

            var accessToken = response.Data.accessToken;

            var resp = await _httpApiService.GetData<GeneralApiResponse<List<ApiShipperResponseObj>>>(
                "Shippers/getallshippers",
                accessToken
            );

            if (resp == null || resp.Data == null)
            {
                ViewBag.Error = "Shipper verileri alınamadı.";
                return View(new List<ApiShipperResponseObj>());
            }

            return View(resp.Data);
        }


        public ViewResult Add()
        {
            return View();
        }

        public ViewResult Save(ShipperAddDto dto)
        {
            var shipper = new Shipper();
            shipper.ShipperName = dto.ShipperName;
            shipper.Phone = dto.Phone;
            using var ctx = new TradewndContext();
            ctx.Shippers.Add(shipper);

            ctx.SaveChanges();
            return View();
        }
    }
}
