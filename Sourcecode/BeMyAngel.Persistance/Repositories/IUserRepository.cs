using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IUserRepository
    {
        UserDto GetById(int UserId);
        UserDto GetByUserName(string UserName);
    }
}
