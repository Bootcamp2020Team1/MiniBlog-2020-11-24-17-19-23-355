using MiniBlog.Model;
using System.Collections.Generic;

namespace MiniBlog.Service
{
    public interface IUserService
    {
        User AddUserByName(string name, string email = "anonymous@unknow.com");
        bool ContainsUser(string name);
        List<User> GetAllUsers();
        User GetUserByName(string name);
        User DeleteUserByName(string name);
        User UpdateUser(User user);
    }
}
