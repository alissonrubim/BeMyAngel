using AutoMapper;
using BeMyAngel.Persistance.Repositories;
using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<Role> GetAll()
        {
            return _mapper.Map<IEnumerable<Role>>(_repository.GetAll());
        }

        public Role GetById(int RoleId)
        {
            return _mapper.Map<Role>(_repository.GetById(RoleId));
        }

        public Role GetByIdentifier(string Identifier)
        {
            return _mapper.Map<Role>(_repository.GetByIdentifier(Identifier));
        }
    }
}
