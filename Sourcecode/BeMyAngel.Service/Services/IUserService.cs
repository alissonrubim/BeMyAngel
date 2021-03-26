using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface IUserService
    {
        User GetById(int UserId);
        User GetByUserName(string UserName);
    }
}
