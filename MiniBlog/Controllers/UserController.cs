using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
            userService.RegisterUserByName(user.Name, user.Email);

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
            return userService.UpdateUser(user);
        }

        [HttpDelete]
        public async Task<ActionResult<User>> Delete(string name)
        {
            var foundUser = userService.GetUserByName(name);
            if (foundUser != null)
            {
                articleService.DeleteArticleByName(name);
                userService.DeleteUserByName(name);
            }

            return Ok(foundUser);
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return userService.GetUserByName(name);
        }
    }
}