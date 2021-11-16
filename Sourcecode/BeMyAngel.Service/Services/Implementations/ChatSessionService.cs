using AutoMapper;
using BeMyAngel.Persistance.Repositories;
using BeMyAngel.Service.Models;
using System;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class ChatSessionService : IChatSessionService
    {
        private readonly IChatSessionRepository _repository;
        private readonly IMapper _mapper;

        public ChatSessionService(IChatSessionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddSessionToChat(int ChatId, int SessionId)
        {
            var token = string.Empty;
            do
            {
                token = Guid.NewGuid().ToString().ToUpper();
            } while (GetByToken(token) != null);
            _repository.Insert(ChatId, SessionId, token);
        }

        public ChatSession Get(int ChatId, int SessionId)
        {
            return _mapper.Map<ChatSession>(_repository.Get(ChatId, SessionId));
        }

        public ChatSession Get(int ChatSessionId)
        {
            return _mapper.Map<ChatSession>(_repository.Get(ChatSessionId));
        }

        public ChatSession GetBySessionId(int SessionId, bool IncludeClosedChats = false)
        {
            return _mapper.Map<ChatSession>(_repository.GetBySessionId(SessionId, IncludeClosedChats));
        }

        public ChatSession GetByToken(string Token)
        {
            return _mapper.Map<ChatSession>(_repository.GetByToken(Token));
        }
    }
}
