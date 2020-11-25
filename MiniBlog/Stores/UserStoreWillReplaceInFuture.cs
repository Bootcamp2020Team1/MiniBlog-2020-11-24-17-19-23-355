using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using MiniBlog.Interfaces;
using MiniBlog.Model;

namespace MiniBlog.Stores
{
    public class UserStore : IUserStore
    {
        public List<User> Users
        {
            get
            {
                return UserStoreWillReplaceInFuture.Users;
            }
        }
    }

    public class UserStoreWillReplaceInFuture
    {
        static UserStoreWillReplaceInFuture()
        {
            Users = new List<User>();
        }

        public static List<User> Users { get; private set; }

        /// <summary>
        /// This is for test only, please help resolve!
        /// </summary>
        public static void Init()
        {
            Users = new List<User>();
        }
    }
}