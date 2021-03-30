using BeMyAngel.Persistance.Models;
using System.Collections.Generic;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IRoleRepository
    {
        RoleDto GetById(int RoleId);
        RoleDto GetByIdentifier(string Identifier);
        IEnumerable<RoleDto> GetAll();
    }
}
