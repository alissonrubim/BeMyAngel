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
    internal class ChatRoomEventService : IChatRoomEventService
    {
        private readonly IChatRoomEventRepository _repository;
        private readonly IChatRoomService _chatRoomService;
        private readonly IChatRoomSessionService _chatRoomSessionService;
        private readonly IMapper _mapper;

        public ChatRoomEventService(IChatRoomEventRepository repository, IChatRoomSessionService chatRoomSessionService, IChatRoomService chatRoomService, IMapper mapper)
        {
            _repository = repository;
            _chatRoomService = chatRoomService;
            _chatRoomSessionService = chatRoomSessionService;
            _mapper = mapper;
        }

        public int CreatePostMessageEvent(int ChatRoomId, string Message, Session Session)
        {
            var chatRoom = _chatRoomService.GetById(ChatRoomId, Session);
            var chatRoomEvent = new ChatRoomEventDto()
            {
                ChatRoomId = chatRoom.ChatRoomId,
                ChatRoomEventTypeId = ChatRoomEventTypes.PostMessage.ChatRoomEventTypeId,
                SessionId = Session.SessionId,
                Data = JsonConvert.SerializeObject(new
                {
                    Message = Message
                })
            };

            return _repository.Insert(chatRoomEvent);
        }

        public IEnumerable<ChatRoomEvent> GetAllByChatRoomId(int ChatRoomId, int SessionId)
        {
            if (_chatRoomSessionService.Get(ChatRoomId, SessionId) == null)
                throw new ForbidException($"SessionId `{SessionId}` it's not part of the ChatRoomId `{ChatRoomId}`");
            return _mapper.Map<IEnumerable<ChatRoomEvent>>(_repository.GetAllByChatRoomId(ChatRoomId));
        }

        public ChatRoomEvent GetById(int ChatRoomEventId)
        {
            return _mapper.Map<ChatRoomEvent>(_repository.GetById(ChatRoomEventId));
        }
    }
}
