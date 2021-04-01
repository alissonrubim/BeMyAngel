using BeMyAngel.Api.Helpers.Authentication;
using BeMyAngel.Service.Models;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BeMyAngel.Api.Helpers.SessionManager
{
    public class SessionManager : ISessionManager
    {
        private readonly string SessionToeknCookieName = "BeMyAngelSessionToken";
        public ISessionService _sessionService;
        public IUserService _userService;

        public SessionManager(ISessionService sessionService, IUserService userService)
        {
            _sessionService = sessionService;
            _userService = userService;
        }

        public Session GetCurrentSession(HttpContext httpContext)
        {
            var cookie = httpContext.Request.Cookies[SessionToeknCookieName];
            if (cookie == null)
                throw new Exception("SessionToken was not found.");

            var session = _sessionService.GetByToken(cookie);
            if (!IsValid(session, httpContext))
                throw new Exception("Session is not valid.");
            return session;
        }

        public void ProcessRequest(ActionExecutingContext filterContext)
        {
            //Get the current session token
            var cookie = filterContext.HttpContext.Request.Cookies[SessionToeknCookieName];
            if (cookie == null)
            {
                //If does not exists, then create a new token
                CreateNewSession(filterContext.HttpContext);
                filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.Path);
            }
            else
            {
                //If exists, check if the token is valid
                var session = _sessionService.GetByToken(cookie);
                if (session != null && IsValid(session, filterContext.HttpContext))
                {
                    _sessionService.Renew(session);

                    //After dealing with the sessions, we check if has a user logged in, if so, attach the user to the session
                    var userId = AuthenticationHelper.GetCurrentUserId(filterContext.HttpContext);
                    if (userId.HasValue)
                    {
                        if (!session.UserId.HasValue)
                        {
                            _sessionService.AttachUser(session, _userService.GetById(userId.Value));
                        }
                        else if(session.UserId.HasValue && session.UserId.Value != userId.Value)
                        {
                            //Something went wrong: a logged user using another user session that is not his session
                        }
                    }
                    else if(session.UserId.HasValue) //If the user is not logged, we remove the user from the session
                    {
                        _sessionService.DeattachUser(session);
                    }
                }
                else
                {
                    CreateNewSession(filterContext.HttpContext);
                    filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.Path);
                }
            }
        }

        private void CreateNewSession(HttpContext httpContext)
        {
            var sessionId = _sessionService.Create(new Session
            {
                LocalIpAddress = httpContext.Connection.LocalIpAddress.ToString(),
                LocalPort = httpContext.Connection.LocalPort,
                RemoteIpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
                RemotePort = httpContext.Connection.RemotePort,
                ConnectionIdentifier = httpContext.Connection.Id,
                UserAgent = httpContext.Request.Headers["User-Agent"]
            });
            var session = _sessionService.GetById(sessionId);
            httpContext.Response.Cookies.Append(SessionToeknCookieName, session.Token, new CookieOptions
            {
                HttpOnly = false,
                Path = "/" ,
                SameSite = SameSiteMode.None,
                Secure = true
            });
        }
        
        private bool IsValid(Session session, HttpContext httpContext)
        {
            return session.LocalIpAddress == httpContext.Connection.LocalIpAddress.ToString() &&
                   session.RemoteIpAddress == httpContext.Connection.RemoteIpAddress.ToString() &&
                   session.UserAgent == httpContext.Request.Headers["User-Agent"];
        }
    }
}
