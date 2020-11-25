using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserStore userStore;
        private readonly List<User> users;

        public UserController(IUserStore userStore)
        {
            this.userStore = userStore;
            users = userStore.Users;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {
            if (!users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(user);
            }

            return CreatedAtAction(nameof(GetByName), new { abc = user.Name }, user);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return users;
        }

        [HttpPut]
        public User Update(User user)
        {
            var foundUser = users.FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        [HttpDelete]
        public User Delete(string name)
        {
            var foundUser = UserStoreWillReplaceInFuture.Users.FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                UserStoreWillReplaceInFuture.Users.Remove(foundUser);
                ArticleStoreWillReplaceInFuture.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return foundUser;
        }

        [HttpGet("{abc}")]
        public User GetByName(string name)
        {
            return UserStoreWillReplaceInFuture.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }
    }
}