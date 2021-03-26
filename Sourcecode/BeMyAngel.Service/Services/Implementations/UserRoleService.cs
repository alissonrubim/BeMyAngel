using AutoMapper;
using BeMyAngel.Persistance.Repositories;
using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository repository, IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public IEnumerable<Role> GetRolesByUserId(int UserId)
        {
            var roles = new List<Role>();
            var userRoles = _repository.GetByUserId(UserId);
            foreach (var userRole in userRoles)
                roles.Add(_mapper.Map<Role>(_roleRepository.GetById(userRole.RoleId)));
            return roles;
        }

        public IEnumerable<User> GetUsersByRoleId(int RoleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersByRoleIdentifier(string Identifier)
        {
            throw new NotImplementedException();
        }
    }
}
