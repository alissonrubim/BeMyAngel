using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Presentations.ChatController;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeMyAngel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [CheckSession]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IChatSessionService _chatSessionService;
        private readonly ISessionManager _sessionManager;

        public ChatController(IChatService ChatService,
                                  IChatSessionService ChatSessionService,
                                  ISessionManager sessionManager)
        {
            _chatService = ChatService;
            _chatSessionService = ChatSessionService;
            _sessionManager = sessionManager;
        }

        [HttpGet("Current")]
        [ProducesResponseType(typeof(GetChatResponse), StatusCodes.Status200OK)]
        public IActionResult GetCurrent()
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            var chat = _chatService.GetCurrentBySession(session);
            if (chat == null)
                return Ok(new GetChatResponse());

            return Ok(new GetChatResponse
            {
                Chat = chat,
                MyChatSessionToken = _chatSessionService.Get(chat.ChatId, session.SessionId).Token
            });
        }

        [Authorize]
        [HttpGet("{ChatId}")]
        [ProducesResponseType(typeof(GetChatResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int chatId)
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            var chat = _chatService.GetById(chatId, session);
            if (chat == null)
                return NotFound();
            var chatSession = _chatSessionService.Get(chat.ChatId, session.SessionId);
            if (chatSession == null)
                return Forbid();
            return Ok(new GetChatResponse
            {
                Chat = chat,
                MyChatSessionToken = chatSession.Token
            });
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(GetChatResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Create()
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            var chat = _chatService.Create(session);
            var chatSession = _chatSessionService.Get(chat.ChatId, session.SessionId);
            return StatusCode(StatusCodes.Status201Created, new GetChatResponse
            {
                Chat = chat,
                MyChatSessionToken = chatSession.Token
            });
        }
    }
}
