using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Presentations.ChatEventController;
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
            var chat = _chatService.GetCurrentBySession(session);

            if (chat == null)
                throw new Exception($"Session {session.Token} does not have an valid chat to connect to.");

            //When a new connection comes in, it going to update the ConnectionId at the ChatSession and asign this new connection to a group
            var chatSession = _chatSessionService.Get(chat.ChatId, session.SessionId);
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
