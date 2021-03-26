using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IUserRoleRepository
    {
        IEnumerable<UserRoleDto> GetByUserId(int UserId);
    }
}
