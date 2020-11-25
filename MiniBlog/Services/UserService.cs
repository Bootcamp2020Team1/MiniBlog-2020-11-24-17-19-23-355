using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class UserService
    {
        public void RegisterUserByName(string userName, string email = "anonymous@unknow.com")
        {
            if (!UserStoreWillReplaceInFuture.Users.Exists(_ => userName.ToLower() == _.Name.ToLower()))
            {
                UserStoreWillReplaceInFuture.Users.Add(new User(userName, email));
            }
        }
    }
}
