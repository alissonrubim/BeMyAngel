using BeMyAngel.Service.Models;
using BeMyAngel.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _service;

        public ChatRoomController(IChatRoomService service)
        {
            _service = service;
        }

        [HttpGet("Current")]
        public ChatRoom GetCurrent(int id)
        {
            return new ChatRoom()
            {
                ChatRoomId = id
            };
            //_service.Get(id);
        }

        [HttpGet("{id}")]
        public ChatRoom Get(int id)
        {
            return new ChatRoom()
            {
                ChatRoomId = id
            };
            //_service.Get(id);
        }
    }
}
