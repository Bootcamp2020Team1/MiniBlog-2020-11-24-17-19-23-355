using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Service;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IArticleService articleService;

        public UserController(IUserService userService, IArticleService articleService)
        {
            this.userService = userService;
            this.articleService = articleService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(User newUser)
        {
            var user = userService.AddUserByName(newUser.Name, newUser.Email);

            return CreatedAtAction(nameof(Register), new { id = user.Name }, user);
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
                userService.DeleteUserByName(name);
                articleService.DeleteArticleByName(name);
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