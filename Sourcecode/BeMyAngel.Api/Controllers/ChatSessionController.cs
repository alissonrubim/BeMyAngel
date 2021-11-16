using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Presentations.ChatSession;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeMyAngel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [CheckSession]
    public class ChatSessionController : ControllerBase
    {
        private readonly IChatSessionService _ChatSessionService;
        private readonly ISessionManager _sessionManager;

        public ChatSessionController(IChatSessionService ChatSessionService,
                                  ISessionManager sessionManager)
        {
            _ChatSessionService = ChatSessionService;
            _sessionManager = sessionManager;
        }

        [Authorize]
        [HttpPost("AssignToChat")]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public void AssignToChat(AssignToChatRequest request)
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            _ChatSessionService.AddSessionToChat(request.ChatId, session.SessionId);
        }
    }
}
