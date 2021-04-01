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
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository repository, IUserService userService, IRoleService roleService, IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public IEnumerable<Role> GetRolesByUserId(int UserId)
        {
            var roles = new List<Role>();
            var userRoles = _repository.GetByUserId(UserId);
            foreach (var userRole in userRoles)
                roles.Add(_mapper.Map<Role>(_roleService.GetById(userRole.RoleId)));
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
