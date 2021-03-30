using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Presentations.ChatRoomController;
using BeMyAngel.Service.Models;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [CheckSession]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IChatRoomEventService _chatRoomEventService;
        private readonly ISessionManager _sessionManager;

        public ChatRoomController(IChatRoomService chatRoomService, IChatRoomEventService chatRoomEventService, ISessionManager sessionManager)
        {
            _chatRoomService = chatRoomService;
            _chatRoomEventService = chatRoomEventService;
            _sessionManager = sessionManager;
        }

        [HttpGet("Current")]
        public GetCurrentResponse GetCurrent()
        {
            var session = _sessionManager.GetCurrentSession(HttpContext);
            var chatRoom = _chatRoomService.GetCurrentBySession(session);
            return new GetCurrentResponse
            {
                ChatRoom = chatRoom,
                ChatRoomEvents = _chatRoomEventService.GetByChatRoomId(chatRoom.ChatRoomId)
            };
        }
    }
}
