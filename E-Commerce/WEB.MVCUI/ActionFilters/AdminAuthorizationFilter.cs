using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using WEB.MVCUI.Areas.Admin.Models.Entities;

public class AdminAuthorizationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var sessionData = context.HttpContext.Session.GetString("LoggedInUser");

        if (sessionData == null)
        {
            context.Result = new RedirectResult("/admin/authentication/login", true);
            return;
        }

        // Gerekirse session'dan user'ı deserialize edebilirsin:
        var loggedInUser = JsonSerializer.Deserialize<User>(sessionData);

        base.OnActionExecuting(context);
    }
}
