using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Presentations.ChatEventController;
using BeMyAngel.Service.Models;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Hubs
{
    public class ChatHub : Hub, IChatHub
    {
        private readonly ISessionManager _sessionManager;
        private readonly IChatService _chatService;
        private readonly IChatSessionService _chatSessionService;
        protected IHubContext<ChatHub> _hubContext;

        public static string[] GetRoutes()
        {
            return new string[]{"/hubs/chat/{ChatSessionToken}"};
        }

        private string GetParameterFromPath(HttpContext httpContext, string parameter) {
            var paths = httpContext.Request.Path.Value.Split('/').Where(x => x != string.Empty).ToList();
            string result = null;

            foreach(var route in GetRoutes())
            {
                var routePaths = route.Split('/').Where(x => x != string.Empty).ToList();
                if(routePaths.Count() == paths.Count())
                {
                    for(var i=0; i< routePaths.Count(); i++)
                    {
                        if (routePaths[i] == $"{{{parameter}}}")
                            result = paths[i];
                    }
                }
            }

            return result;
        }

        public ChatHub(IHubContext<ChatHub> hubContext, ISessionManager sessionManager, IChatService chatService, IChatSessionService chatSessionService): base()
        {
            _sessionManager = sessionManager;
            _chatService = chatService;
            _chatSessionService = chatSessionService;
            _hubContext = hubContext;
        }

        public override Task OnConnectedAsync()
        {
            var session = _sessionManager.GetCurrentSession(Context.GetHttpContext());
            var chatSessionToken = GetParameterFromPath(Context.GetHttpContext(), "ChatSessionToken");
            var chatSession = _chatSessionService.GetByToken(chatSessionToken);
            
            if (chatSession == null)
                throw new Exception($"Session {session.Token} does not have an valid chat session to connect to.");

            //When a new connection comes in, it going to update the ConnectionId at the ChatSession and asign this new connection to a group
            var chat = _chatService.GetById(chatSession.ChatId, session);
            if(chatSession.ConnectionId == null || chatSession.ConnectionId != Context.ConnectionId)
            {
                _chatSessionService.DefineConnectionId(chatSession.ChatSessionId, Context.ConnectionId);
                JoinToGroup(chat.Identifier, Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendMessage(string chatIdentifier, string chatSessionToken, HubEventResponse eventResponse){

            return _hubContext.Clients.Group(chatIdentifier).SendAsync("ReceiveMessage", chatSessionToken, eventResponse);
        }

        public Task JoinToGroup(string chatIdentifier, string ConnectionId)
        {
            return _hubContext.Groups.AddToGroupAsync(ConnectionId, chatIdentifier);
        }
    }
}
