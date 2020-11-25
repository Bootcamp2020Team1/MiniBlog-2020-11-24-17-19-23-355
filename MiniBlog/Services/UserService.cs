using Microsoft.AspNetCore.Identity;
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
        private IUserStore userStore;

        public UserService(IUserStore userStore)
        {
            this.userStore = userStore;
        }

        public void RegisterUserByName(string userName, string email = "anonymous@unknow.com")
        {
            if (!userStore.Users.Exists(_ => userName.ToLower() == _.Name.ToLower()))
            {
                //UserStoreWillReplaceInFuture.Users.Add(user);
                userStore.Users.Add(new User(userName, email));
            }
        }
    }
}