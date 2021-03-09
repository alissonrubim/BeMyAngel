using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services.ChatRoomService
{
    public interface IChatRoomService
    {
        ChatRoom GetCurrent();
        ChatRoom Get(int id);
    }
}
