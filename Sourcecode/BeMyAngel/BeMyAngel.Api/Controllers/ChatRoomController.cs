using BeMyAngel.Service.Models;
using BeMyAngel.Service.Services;
using BeMyAngel.Service.Services.ChatRoomService;
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

        [HttpGet("{id}")]
        public ChatRoom Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
