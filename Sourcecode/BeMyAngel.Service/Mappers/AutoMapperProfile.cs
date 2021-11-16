using AutoMapper;
using BeMyAngel.Service.Models;
using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Service.Mappers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Chat, ChatDto>();
            CreateMap<ChatDto, Chat>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<Session, SessionDto>();
            CreateMap<SessionDto, Session>();

            CreateMap<ChatEvent, ChatEventDto>();
            CreateMap<ChatEventDto, ChatEvent>();

            CreateMap<ChatSession, ChatSessionDto>();
            CreateMap<ChatSessionDto, ChatSession>();
        }
    }
}
