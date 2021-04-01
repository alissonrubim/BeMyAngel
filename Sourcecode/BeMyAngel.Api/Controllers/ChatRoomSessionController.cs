using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Presentations.ChatRoomController;
using BeMyAngel.Api.Presentations.ChatRoomSession;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeMyAngel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [CheckSession]
    public class ChatRoomSessionController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IChatRoomSessionService _chatRoomSessionService;
        private readonly ISessionManager _sessionManager;

        public ChatRoomSessionController(IChatRoomService chatRoomService, 
                                  IChatRoomSessionService chatRoomSessionService,
                                  ISessionManager sessionManager)
        {
            _chatRoomService = chatRoomService;
            _chatRoomSessionService = chatRoomSessionService;
            _sessionManager = sessionManager;
        }

        [HttpPost("AssignToChatRoom")]
        [Authorize]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public void AssignToChatRoom(AssignToChatRoomRequest request)
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            _chatRoomSessionService.AddSessionToChatRoom(request.ChatRoomId, session.SessionId);
        }
    }
}
