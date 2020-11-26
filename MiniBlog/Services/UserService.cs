using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly IUserStore userStore;

        public UserService(IUserStore userStore)
        {
            this.userStore = userStore;
        }

        public void RegisterUserByName(string userName, string email = "anonymous@unknow.com")
        {
            if (!userStore.Users.Exists(_ => userName.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(new User(userName, email));
            }
        }

        public List<User> GetAllUsers()
        {
            return userStore.Users;
        }

        public User UpdateUser(User user)
        {
            var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
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

        public User GetUserByName(string name)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }
    }
}
