using AutoMapper;
using BeMyAngel.Service.Models;
using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Service.Mappers
{
    public class ChatRoomProfile: Profile
    {
        public ChatRoomProfile()
        {
            CreateMap<ChatRoom, ChatRoomDto>();
            CreateMap<ChatRoomDto, ChatRoom>();
        }
    }
}
