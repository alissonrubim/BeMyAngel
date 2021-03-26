using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface IUserRoleService
    {
        IEnumerable<Role> GetRolesByUserId(int UserId);
        IEnumerable<User> GetUsersByRoleId(int RoleId);
        IEnumerable<User> GetUsersByRoleIdentifier(string Identifier);
    }
}
