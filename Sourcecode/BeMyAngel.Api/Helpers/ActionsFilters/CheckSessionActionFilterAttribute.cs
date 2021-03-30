using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Helpers.SessionManager
{
    public class CheckSession: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _sessionManager = (ISessionManager)context.HttpContext.RequestServices.GetService(typeof(ISessionManager));
            _sessionManager.ProcessRequest(context.HttpContext);
        }
    }
}
