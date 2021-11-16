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
        private readonly IChatEventService _ChatEventService;
        private readonly IChatSessionService _ChatSessionService;

        public ChatEventController(IChatHub chatHub, 
                                       ISessionManager sessionManager, 
                                       IChatEventService ChatEventService,
                                       IChatSessionService ChatSessionService)
        {
            _chatHub = chatHub;
            _sessionManager = sessionManager;
            _ChatEventService = ChatEventService;
            _ChatSessionService = ChatSessionService;
        }

        [HttpGet("{ChatId}")]
        [ProducesResponseType(typeof(IEnumerable<HubEventResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public IActionResult GetAll(int ChatId)
        {
            var result = new List<HubEventResponse>();
            try
            {
                var session = _sessionManager.GetCurrentSession(HttpContext);
                var ChatEvents = _ChatEventService.GetAllByChatId(ChatId, session.SessionId);
                foreach (var ChatEvent in ChatEvents)
                {
                    var ChatSession = _ChatSessionService.Get(ChatEvent.ChatSessionId);
                    result.Add(new HubEventResponse
                    {
                        ChatEventId = ChatEvent.ChatEventId,
                        ChatEventTypeId = ChatEvent.ChatEventTypeId,
                        ChatSessionToken = ChatSession.Token,
                        CreatedAt = ChatEvent.CreatedAt,
                        Data = ChatEvent.Data
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
            var ChatEventId = _ChatEventService.CreatePostMessageEvent(request.ChatId, request.Message, session);
            var ChatEvent = _ChatEventService.GetById(ChatEventId);
            var ChatSession = _ChatSessionService.Get(ChatEvent.ChatId, session.SessionId);
            var result = new HubEventResponse
            {
                ChatEventId = ChatEvent.ChatEventId,
                ChatEventTypeId = ChatEvent.ChatEventTypeId,
                ChatSessionToken = ChatSession.Token,
                CreatedAt = ChatEvent.CreatedAt,
                Data = ChatEvent.Data
            };

            //_chatHub.Clients.All.ReceiveMessage(result);
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
