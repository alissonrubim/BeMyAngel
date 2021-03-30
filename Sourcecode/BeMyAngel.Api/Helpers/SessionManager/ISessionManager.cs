using BeMyAngel.Service.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Helpers.SessionManager
{
    public interface ISessionManager
    {
        void ProcessRequest(HttpContext httpContext);
        Session GetCurrentSession(HttpContext httpContext);
    }
}
