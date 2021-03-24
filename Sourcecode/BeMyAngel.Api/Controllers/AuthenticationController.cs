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
    public class AuthenticationController : ControllerBase
    {

        [HttpPost("GetSession")]
        public void GetSession(Guid SessionToken)
        {

        }

        [HttpPost("Login")]
        public void Login()
        {
            
        }

        [HttpPost("Logout")]
        public void Logout()
        {

        }
    }
}
