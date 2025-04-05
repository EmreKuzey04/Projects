using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using WEB.MVCUI.Models.Entities;

namespace WEB.MVCUI.ActionFilters
{
    public class CheckSession:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var jsonStr = context.HttpContext.Session.GetString("ActiveAppUser");
            if (!string.IsNullOrEmpty(jsonStr))
            {
                var appUser = JsonSerializer.Deserialize<AppUser>(jsonStr);
                if (appUser == null)
                {
                    context.Result = new RedirectToActionResult("LogIn","AppUser",null);
                }
            }
            else
            context.Result = new RedirectToActionResult("LogIn", "AppUser", null);
        }
    }
}
