using AutoMapper;
using BeMyAngel.Persistance.Models;
using BeMyAngel.Persistance.Repositories;
using BeMyAngel.Service.Exceptions;
using BeMyAngel.Service.Models;
using BeMyAngel.Service.Models.Collections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class ChatEventService : IChatEventService
    {
        private readonly IChatEventRepository _repository;
        private readonly IChatService _ChatService;
        private readonly IChatSessionService _ChatSessionService;
        private readonly IMapper _mapper;

        public ChatEventService(IChatEventRepository repository, IChatSessionService ChatSessionService, IChatService ChatService, IMapper mapper)
        {
            _repository = repository;
            _ChatService = ChatService;
            _ChatSessionService = ChatSessionService;
            _mapper = mapper;
        }

        public int CreatePostMessageEvent(int ChatId, string Message, Session Session)
        {
            var Chat = _ChatService.GetById(ChatId, Session);
            var ChatSession = _ChatSessionService.Get(ChatId, Session.SessionId);
            var ChatEvent = new ChatEventDto()
            {
                CreatedAt = DateTime.Now,
                ChatId = Chat.ChatId,
                ChatEventTypeId = ChatEventTypes.PostMessage.ChatEventTypeId,
                ChatSessionId = ChatSession.ChatSessionId,
                Data = JsonConvert.SerializeObject(new
                {
                    Message = Message
                })
            };

            return _repository.Insert(ChatEvent);
        }

        public IEnumerable<ChatEvent> GetAllByChatId(int ChatId, int SessionId)
        {
            if (_ChatSessionService.Get(ChatId, SessionId) == null)
                throw new ForbidException($"SessionId `{SessionId}` it's not part of the ChatId `{ChatId}`");
            return _mapper.Map<IEnumerable<ChatEvent>>(_repository.GetAllByChatId(ChatId));
        }

        public ChatEvent GetById(int ChatEventId)
        {
            return _mapper.Map<ChatEvent>(_repository.GetById(ChatEventId));
        }
    }
}
