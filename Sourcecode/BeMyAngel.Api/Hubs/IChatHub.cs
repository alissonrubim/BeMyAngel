using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Hubs
{
    public interface IChatHub
    {
        Task ReceiveMessage(ChatRoomEvent chatRoomEvent);
    }
}
