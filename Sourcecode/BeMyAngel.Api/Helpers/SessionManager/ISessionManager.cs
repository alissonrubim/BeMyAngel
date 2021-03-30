using BeMyAngel.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeMyAngel.Api.Helpers.SessionManager
{
    public interface ISessionManager
    {
        void ProcessRequest(ActionExecutingContext filterContext);
        Session GetCurrentSession(HttpContext httpContext);
    }
}
