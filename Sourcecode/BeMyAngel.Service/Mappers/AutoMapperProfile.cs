using AutoMapper;
using BeMyAngel.Service.Models;
using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Service.Mappers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ChatRoom, ChatRoomDto>();
            CreateMap<ChatRoomDto, ChatRoom>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<Session, SessionDto>();
            CreateMap<SessionDto, Session>();

            CreateMap<ChatRoomEvent, ChatRoomEventDto>();
            CreateMap<ChatRoomEventDto, ChatRoomEvent>();
        }
    }
}
