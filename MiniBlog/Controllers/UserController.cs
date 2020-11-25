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
        public UserController(IUserStore userStore)
        {
            this.userStore = userStore;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {
            if (!userStore.Users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(user);
            }

            return CreatedAtAction("Register", new { id = user.Name });
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return UserStoreWillReplaceInFuture.Users;
        }

        [HttpPut]
        public User Update(User user)
        {
            var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        [HttpDelete]
        public async Task<ActionResult<User>> Delete(string name)
        {
            var foundUser = UserStoreWillReplaceInFuture.Users.FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                UserStoreWillReplaceInFuture.Users.Remove(foundUser);
                ArticleStoreWillReplaceInFuture.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return Ok(foundUser);
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return UserStoreWillReplaceInFuture.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }
    }
}