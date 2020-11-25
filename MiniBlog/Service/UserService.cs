using MiniBlog.Model;
using MiniBlog.Stores;
using System.Collections.Generic;
using System.Linq;
namespace MiniBlog.Service
{
    public interface IUserService
    {
        User AddUserByName(string name, string email);
        bool ContainsUser(string name);
        List<User> GetAllUsers();
        User GetUserByName(string name);
        User DeleteUserByName(string name);
    }

    public class UserService : IUserService
    {
        private readonly IUserStore userStore;
        private readonly IArticleStore articleStore;
        public UserService(IUserStore userStore, IArticleStore articleStore)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
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
                articleStore.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return foundUser;
        }
    }
}
