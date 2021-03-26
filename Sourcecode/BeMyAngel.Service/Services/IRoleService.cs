using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface IRoleService
    {
        Role GetById(int RoleId);
        Role GetByIdentifier(string Identifier);
        IEnumerable<Role> GetAll();
    }
}
