using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Presentations.ChatRoomController;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeMyAngel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [CheckSession]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IChatRoomSessionService _chatRoomSessionService;
        private readonly ISessionManager _sessionManager;

        public ChatRoomController(IChatRoomService chatRoomService, 
                                  IChatRoomSessionService chatRoomSessionService,
                                  ISessionManager sessionManager)
        {
            _chatRoomService = chatRoomService;
            _chatRoomSessionService = chatRoomSessionService;
            _sessionManager = sessionManager;
        }

        [HttpGet("Current")]
        [ProducesResponseType(typeof(GetCurrentResponse), StatusCodes.Status200OK)]
        public IActionResult GetCurrent()
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            var chatRoom = _chatRoomService.GetCurrentBySession(session);
            return Ok(new GetCurrentResponse
            {
                ChatRoom = chatRoom,
                MyChatRoomSessionToken = _chatRoomSessionService.Get(chatRoom.ChatRoomId, session.SessionId).Token
            });
        }

        [Authorize]
        [HttpGet("{ChatRoomId}")]
        [ProducesResponseType(typeof(GetCurrentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public IActionResult GetById(int ChatRoomId)
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            var chatRoom = _chatRoomService.GetById(ChatRoomId, session);
            return Ok(new GetCurrentResponse
            {
                ChatRoom = chatRoom,
                MyChatRoomSessionToken = _chatRoomSessionService.Get(chatRoom.ChatRoomId, session.SessionId).Token
            });
        }
    }
}
