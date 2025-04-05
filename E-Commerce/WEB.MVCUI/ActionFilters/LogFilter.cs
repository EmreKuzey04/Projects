using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB.MVCUI.ActionFilters
{
    public class LogFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

    }
}
