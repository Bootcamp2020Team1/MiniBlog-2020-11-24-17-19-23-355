using Microsoft.AspNetCore.Identity;
using MiniBlog.Interfaces;
using MiniBlog.Model;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly IUserStore userStore;
        public UserService(IUserStore userStore)
        {
            this.userStore = userStore;
        }

        internal void AddNewUser(string userName, string email = "anonymous@unknow.com")
        {
            if (!userStore.Users.Exists(_ => userName == _.Name))
            {
                userStore.Users.Add(new User(userName, email));
            }
        }

        internal List<User> GetAllUsers()
        {
            return userStore.Users;
        }

        internal User FindUserByName(string name)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name == name);
        }

        internal void Remove(User user)
        {
            userStore.Users.Remove(user);
        }
    }
}
