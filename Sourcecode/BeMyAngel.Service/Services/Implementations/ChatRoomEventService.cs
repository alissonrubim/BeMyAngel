using AutoMapper;
using BeMyAngel.Persistance.Repositories;
using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class ChatRoomEventService : IChatRoomEventService
    {
        private readonly IChatRoomEventRepository _repository;
        private readonly IMapper _mapper;

        public ChatRoomEventService(IChatRoomEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<ChatRoomEvent> GetByChatRoomId(int ChatRoomId)
        {
            return _mapper.Map<IEnumerable<ChatRoomEvent>>(_repository.GetByChatRoomId(ChatRoomId));
        }
    }
}
