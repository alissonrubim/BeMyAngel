using BeMyAngel.Persistance.Models;
using System.Collections.Generic;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IUserRoleRepository
    {
        IEnumerable<UserRoleDto> GetByUserId(int UserId);
    }
}
