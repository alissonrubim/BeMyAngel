using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Hubs;
using BeMyAngel.Api.Presentations.Chat;
using BeMyAngel.Service.Models;
using BeMyAngel.Service.Models.Collections;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [CheckSession]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatHub> _chatHub;
        private readonly ISessionManager _sessionManager;
        private readonly IChatRoomService _chatRoomService;

        public ChatController(IHubContext<ChatHub, IChatHub> chatHub, ISessionManager sessionManager, IChatRoomService chatRoomService)
        {
            _chatHub = chatHub;
            _sessionManager = sessionManager;
            _chatRoomService = chatRoomService;
        }

        [HttpPost("Event")]
        public async Task PostMessage(PostMessageRequest request)
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            var chatRoomEvent = new ChatRoomEvent()
            {
                ChatRoomId = _chatRoomService.GetCurrentBySession(session).ChatRoomId,
                ChatRoomEventTypeId = ChatRoomEventTypes.PostMessage.ChatRoomEventTypeId,
                SessionId = session.SessionId,
                Data = JsonConvert.SerializeObject(new {
                    Message = request.Message
                })
            };
            await _chatHub.Clients.All.ReceiveMessage(chatRoomEvent);
        }
    }
}
