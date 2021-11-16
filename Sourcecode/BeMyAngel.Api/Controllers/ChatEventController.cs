using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Hubs;
using BeMyAngel.Api.Presentations.ChatEventController;
using BeMyAngel.Service.Exceptions;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [CheckSession]
    public class ChatEventController : ControllerBase
    {
        private readonly IChatHub _chatHub;
        private readonly ISessionManager _sessionManager;
        private readonly IChatService _chatService;
        private readonly IChatEventService _chatEventService;
        private readonly IChatSessionService _chatSessionService;

        public ChatEventController(IChatHub chatHub, 
                                       ISessionManager sessionManager, 
                                       IChatService chatService,
                                       IChatEventService chatEventService,
                                       IChatSessionService chatSessionService)
        {
            _chatHub = chatHub;
            _sessionManager = sessionManager;
            _chatService = chatService;
            _chatEventService = chatEventService;
            _chatSessionService = chatSessionService;
        }

        [HttpGet("{ChatId}")]
        [ProducesResponseType(typeof(IEnumerable<HubEventResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public IActionResult GetAll(int chatId)
        {
            var result = new List<HubEventResponse>();
            try
            {
                var session = _sessionManager.GetCurrentSession(HttpContext);
                var chatEvents = _chatEventService.GetAllByChatId(chatId, session.SessionId);
                foreach (var chatEvent in chatEvents)
                {
                    var chatSession = _chatSessionService.Get(chatEvent.ChatSessionId);
                    result.Add(new HubEventResponse
                    {
                        ChatEventId = chatEvent.ChatEventId,
                        ChatEventTypeId = chatEvent.ChatEventTypeId,
                        ChatSessionToken = chatSession.Token,
                        CreatedAt = chatEvent.CreatedAt,
                        Data = chatEvent.Data
                    });
                }
            }
            catch (ForbidException e)
            {
                return Forbid();
            }
            return Ok(result);
        }

        [HttpPost("PostMessage")]
        [ProducesResponseType(typeof(HubEventResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public IActionResult PostMessage(PostMessageRequest request)
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            var chat = _chatService.GetById(request.ChatId, session); 
            var chatEventId = _chatEventService.CreatePostMessageEvent(chat.ChatId, request.Message, session);
            var chatEvent = _chatEventService.GetById(chatEventId);
            var chatSession = _chatSessionService.Get(chatEvent.ChatId, session.SessionId);
            var result = new HubEventResponse
            {
                ChatEventId = chatEvent.ChatEventId,
                ChatEventTypeId = chatEvent.ChatEventTypeId,
                ChatSessionToken = chatSession.Token,
                CreatedAt = chatEvent.CreatedAt,
                Data = chatEvent.Data
            };

            _chatHub.SendMessage(chat.Identifier, chatSession.Token, result);

            return Created(string.Empty, result);
        }


        [HttpPost("IsTypping")]
        public async Task IsTypping()
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            /*var ChatEventId = _ChatEventService.CreateIsTyppingEvent(session);
            var ChatEvent = _ChatEventService.GetById(ChatEventId);

            await _chatHub.Clients.All.ReceiveMessage(new HubEventResponse
            {
                ChatEventTypeId = ChatEvent.ChatEventTypeId,
                ChatSessionToken = _ChatSessionService.Get(ChatEvent.ChatId, session.SessionId).Token,
                CreatedAt = ChatEvent.CreatedAt,
                Data = ChatEvent.Data
            });*/
        }
    }
}
