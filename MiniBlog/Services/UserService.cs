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
        private IArticleStore articleStore;

        public UserService(IUserStore userStore, IArticleStore articleStore)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
        }

        public void RegisterUserByName(string userName, string email = "anonymous@unknow.com")
        {
            if (!userStore.Users.Exists(_ => userName.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(new User(userName, email));
            }
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

        public User DeleteUser(string name)
        {
            var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                userStore.Users.Remove(foundUser);
                articleStore.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return foundUser;
        }

        public User GetUserByName(string name)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }
    }
}