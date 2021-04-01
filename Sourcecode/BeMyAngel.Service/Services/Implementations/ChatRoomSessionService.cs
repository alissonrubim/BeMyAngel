using AutoMapper;
using BeMyAngel.Persistance.Repositories;
using BeMyAngel.Service.Models;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class ChatRoomSessionService : IChatRoomSessionService
    {
        private readonly IChatRoomSessionRepository _repository;
        private readonly IMapper _mapper;

        public ChatRoomSessionService(IChatRoomSessionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddSessionToChatRoom(int ChatRoomId, int SessionId)
        {
            _repository.AddSessionToChatRoom(ChatRoomId, SessionId);
        }

        public ChatRoomSession Get(int ChatRoomId, int SessionId)
        {
            return _mapper.Map<ChatRoomSession>(_repository.Get(ChatRoomId, SessionId));
        }

        public ChatRoomSession GetBySessionId(int SessionId, bool IncludeClosedChatRooms = false)
        {
            return _mapper.Map<ChatRoomSession>(_repository.GetBySessionId(SessionId, IncludeClosedChatRooms));
        }

        public ChatRoomSession GetByToken(string Token)
        {
            return _mapper.Map<ChatRoomSession>(_repository.GetByToken(Token));
        }
    }
}
