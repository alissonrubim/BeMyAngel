using AutoMapper;
using BeMyAngel.Persistance.Repositories;
using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public User GetById(int UserId)
        {
            return _mapper.Map<User>(_repository.GetById(UserId));
        }

        public User GetByUserName(string UserName)
        {
            return _mapper.Map<User>(_repository.GetByUserName(UserName));
        }
    }
}
