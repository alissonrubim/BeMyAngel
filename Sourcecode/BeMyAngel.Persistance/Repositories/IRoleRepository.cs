using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IRoleRepository
    {
        RoleDto GetById(int RoleId);
        RoleDto GetByIdentifier(string Identifier);
        IEnumerable<RoleDto> GetAll();
    }
}
