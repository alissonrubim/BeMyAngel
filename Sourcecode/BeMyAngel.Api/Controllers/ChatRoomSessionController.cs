using BeMyAngel.Api.Helpers.SessionManager;
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
        private readonly IChatRoomSessionService _chatRoomSessionService;
        private readonly ISessionManager _sessionManager;

        public ChatRoomSessionController(IChatRoomSessionService chatRoomSessionService,
                                  ISessionManager sessionManager)
        {
            _chatRoomSessionService = chatRoomSessionService;
            _sessionManager = sessionManager;
        }

        [Authorize]
        [HttpPost("AssignToChatRoom")]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public void AssignToChatRoom(AssignToChatRoomRequest request)
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            _chatRoomSessionService.AddSessionToChatRoom(request.ChatRoomId, session.SessionId);
        }
    }
}
