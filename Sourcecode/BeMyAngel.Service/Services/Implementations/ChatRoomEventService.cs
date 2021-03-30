using AutoMapper;
using BeMyAngel.Persistance.Models;
using BeMyAngel.Persistance.Repositories;
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
        private readonly IMapper _mapper;

        public ChatRoomEventService(IChatRoomEventRepository repository, IChatRoomService chatRoomService, IMapper mapper)
        {
            _repository = repository;
            _chatRoomService = chatRoomService;
            _mapper = mapper;
        }

        public int CreatePostMessageEvent(Session Session, string Message)
        {
            var chatRoomEvent = new ChatRoomEventDto()
            {
                ChatRoomId = _chatRoomService.GetCurrentBySession(Session).ChatRoomId,
                ChatRoomEventTypeId = ChatRoomEventTypes.PostMessage.ChatRoomEventTypeId,
                SessionId = Session.SessionId,
                Data = JsonConvert.SerializeObject(new
                {
                    Message = Message
                })
            };

            return _repository.Insert(chatRoomEvent);
        }

        public IEnumerable<ChatRoomEvent> GetAllByChatRoomId(int ChatRoomId)
        {
            return _mapper.Map<IEnumerable<ChatRoomEvent>>(_repository.GetAllByChatRoomId(ChatRoomId));
        }

        public ChatRoomEvent GetById(int ChatRoomEventId)
        {
            return _mapper.Map<ChatRoomEvent>(_repository.GetById(ChatRoomEventId));
        }
    }
}
