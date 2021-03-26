using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IUserRepository
    {
        UserDto GetById(int UserId);
        UserDto GetByUserName(string UserName);
    }
}
