using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Hubs;
using BeMyAngel.Api.Presentations.Chat;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [CheckSession]
    public class ChatRoomEventController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatHub> _chatHub;
        private readonly ISessionManager _sessionManager;
        private readonly IChatRoomEventService _chatRoomEventService;

        public ChatRoomEventController(IHubContext<ChatHub, IChatHub> chatHub, 
                                       ISessionManager sessionManager, 
                                       IChatRoomEventService chatRoomEventService)
        {
            _chatHub = chatHub;
            _sessionManager = sessionManager;
            _chatRoomEventService = chatRoomEventService;
        }

        [HttpPost("PostMessage")]
        public async Task PostMessage(PostMessageRequest request)
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            var chatRoomEventId = _chatRoomEventService.CreatePostMessageEvent(session, request.Message);
            await _chatHub.Clients.All.ReceiveMessage(_chatRoomEventService.GetById(chatRoomEventId));
        }
    }
}
