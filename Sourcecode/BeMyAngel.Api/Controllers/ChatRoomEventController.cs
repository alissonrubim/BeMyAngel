using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Hubs;
using BeMyAngel.Api.Presentations.ChatRoomEventController;
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
    public class ChatRoomEventController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatHub> _chatHub;
        private readonly ISessionManager _sessionManager;
        private readonly IChatRoomEventService _chatRoomEventService;
        private readonly IChatRoomSessionService _chatRoomSessionService;

        public ChatRoomEventController(IHubContext<ChatHub, IChatHub> chatHub, 
                                       ISessionManager sessionManager, 
                                       IChatRoomEventService chatRoomEventService,
                                       IChatRoomSessionService chatRoomSessionService)
        {
            _chatHub = chatHub;
            _sessionManager = sessionManager;
            _chatRoomEventService = chatRoomEventService;
            _chatRoomSessionService = chatRoomSessionService;
        }

        [HttpGet("{ChatRoomId}")]
        [ProducesResponseType(typeof(IEnumerable<HubEventResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public IActionResult GetAll(int ChatRoomId)
        {
            var result = new List<HubEventResponse>();
            try
            {
                var session = _sessionManager.GetCurrentSession(HttpContext);
                var chatRoomEvents = _chatRoomEventService.GetAllByChatRoomId(ChatRoomId, session.SessionId);
                foreach (var chatRoomEvent in chatRoomEvents)
                {
                    var chatRoomSession = _chatRoomSessionService.Get(chatRoomEvent.ChatRoomSessionId);
                    result.Add(new HubEventResponse
                    {
                        ChatRoomEventId = chatRoomEvent.ChatRoomEventId,
                        ChatRoomEventTypeId = chatRoomEvent.ChatRoomEventTypeId,
                        ChatRoomSessionToken = chatRoomSession.Token,
                        CreatedAt = chatRoomEvent.CreatedAt,
                        Data = chatRoomEvent.Data
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
            var chatRoomEventId = _chatRoomEventService.CreatePostMessageEvent(request.ChatRoomId, request.Message, session);
            var chatRoomEvent = _chatRoomEventService.GetById(chatRoomEventId);
            var chatRoomSession = _chatRoomSessionService.Get(chatRoomEvent.ChatRoomId, session.SessionId);
            var result = new HubEventResponse
            {
                ChatRoomEventId = chatRoomEvent.ChatRoomEventId,
                ChatRoomEventTypeId = chatRoomEvent.ChatRoomEventTypeId,
                ChatRoomSessionToken = chatRoomSession.Token,
                CreatedAt = chatRoomEvent.CreatedAt,
                Data = chatRoomEvent.Data
            };
            _chatHub.Clients.All.ReceiveMessage(result).Wait();
            return Created(string.Empty, result);
        }


        [HttpPost("IsTypping")]
        public async Task IsTypping()
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            /*var chatRoomEventId = _chatRoomEventService.CreateIsTyppingEvent(session);
            var chatRoomEvent = _chatRoomEventService.GetById(chatRoomEventId);

            await _chatHub.Clients.All.ReceiveMessage(new HubEventResponse
            {
                ChatRoomEventTypeId = chatRoomEvent.ChatRoomEventTypeId,
                ChatRoomSessionToken = _chatRoomSessionService.Get(chatRoomEvent.ChatRoomId, session.SessionId).Token,
                CreatedAt = chatRoomEvent.CreatedAt,
                Data = chatRoomEvent.Data
            });*/
        }
    }
}
