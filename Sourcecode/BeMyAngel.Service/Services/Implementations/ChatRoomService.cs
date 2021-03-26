using AutoMapper;
using BeMyAngel.Service.Models;
using BeMyAngel.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class ChatRoomService: IChatRoomService
    {
        private readonly IChatRoomRepository _repository;
        private readonly IMapper _mapper;

        public ChatRoomService(IChatRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ChatRoom Get(int id)
        {
            return _mapper.Map<ChatRoom>(_repository.Get(1, id));
        }

        public ChatRoom GetCurrent()
        {
            return _mapper.Map<ChatRoom>(_repository.GetCurrent(1));
        }
    }
}
