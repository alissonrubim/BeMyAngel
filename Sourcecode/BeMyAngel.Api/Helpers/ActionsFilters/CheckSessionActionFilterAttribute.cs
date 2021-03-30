using Microsoft.AspNetCore.Mvc.Filters;

namespace BeMyAngel.Api.Helpers.SessionManager
{
    public class CheckSession: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _sessionManager = (ISessionManager)context.HttpContext.RequestServices.GetService(typeof(ISessionManager));
            _sessionManager.ProcessRequest(context);
        }
    }
}
