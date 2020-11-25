using MiniBlog.Model;
using MiniBlog.Stores;
using System.Collections.Generic;
using System.Linq;
namespace MiniBlog.Service
{
    public class UserService : IUserService
    {
        private readonly IUserStore userStore;
        public UserService(IUserStore userStore)
        {
            this.userStore = userStore;
        }

        public User AddUserByName(string name, string email = "anonymous@unknow.com")
        {
            if (!ContainsUser(name))
            {
                var newUser = new User(name, email);
                userStore.Users.Add(newUser);
                return newUser;
            }

            return GetUserByName(name);
        }

        public bool ContainsUser(string name)
        {
            return userStore.Users.Exists(_ => name.ToLower() == _.Name.ToLower());
        }

        public User GetUserByName(string name)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }

        public List<User> GetAllUsers()
        {
            return userStore.Users;
        }

        public User DeleteUserByName(string name)
        {
            var foundUser = GetUserByName(name);
            if (foundUser != null)
            {
                userStore.Users.Remove(foundUser);
            }

            return foundUser;
        }

        public User UpdateUser(User user)
        {
            var foundUser = GetUserByName(user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }
    }
}
