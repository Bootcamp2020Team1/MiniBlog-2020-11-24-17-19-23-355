using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MiniBlog.Interfaces;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly ArticleService articleService;
        public UserController(UserService userService, ArticleService articleService)
        {
            this.userService = userService;
            this.articleService = articleService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {
            userService.AddNewUser(user.Name, user.Email);

            return CreatedAtAction(nameof(GetByName), new { name = user.Name }, user);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return userService.GetAllUsers();
        }

        [HttpPut]
        public User Update(User user)
        {
            var foundUser = userService.FindUserByName(user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        [HttpDelete]
        public User Delete(string name)
        {
            var foundUser = userService.FindUserByName(name);
            if (foundUser != null)
            {
                userService.Remove(foundUser);
                articleService.RemoveAllArticlesOfUser(foundUser);
            }

            return foundUser;
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return userService.FindUserByName(name);
        }
    }
}