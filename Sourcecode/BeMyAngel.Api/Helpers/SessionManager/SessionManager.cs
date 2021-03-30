using BeMyAngel.Api.Helpers.Authentication;
using BeMyAngel.Service.Models;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void ProcessRequest(HttpContext httpContext)
        {
            //Get the current session token
            var cookie = httpContext.Request.Cookies[SessionToeknCookieName];
            if (cookie == null)
            {
                //If does not exists, then create a new token
                CreateNewSession(httpContext);
            }
            else
            {
                //If exists, check if the token is valid
                var session = _sessionService.GetByToken(cookie);
                if (IsValid(session, httpContext))
                {
                    _sessionService.Renew(session);

                    //After dealing with the sessions, we check if has a user logged in, if so, attach the user to the session
                    var userId = AuthenticationHelper.GetCurrentUserId(httpContext);
                    if (userId.HasValue)
                    {
                        if (!session.UserId.HasValue)
                        {
                            _sessionService.AttachToUser(session, _userService.GetById(userId.Value));
                        }
                        else if(session.UserId.HasValue && session.UserId.Value != userId.Value)
                        {
                            //Something went wrong: a logged user using another user session
                        }
                    }
                    else if(session.UserId.HasValue) //If the user is not logged
                    {
                        //Do nothing for now
                    }
                }
                else
                {
                    CreateNewSession(httpContext);
                }
            }
        }

        private void CreateNewSession(HttpContext httpContext)
        {
            var sessionId = _sessionService.Create(new Session
            {
                IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
                UserAgent = httpContext.Request.Headers["User-Agent"]
            });
            var session = _sessionService.GetById(sessionId);
            httpContext.Response.Cookies.Append(SessionToeknCookieName, session.Token);
            httpContext.Response.Redirect(httpContext.Request.Path);
        }
        
        private bool IsValid(Session session, HttpContext httpContext)
        {
            return session.IpAddress == httpContext.Connection.RemoteIpAddress.ToString() &&
                   session.UserAgent == httpContext.Request.Headers["User-Agent"];
        }
    }
}
