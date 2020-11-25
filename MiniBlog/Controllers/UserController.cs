using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Service;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService, IArticleStore articleStore)
        {
            this.userService = userService;
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
            var foundUser = GetByName(user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        [HttpDelete]
        public async Task<ActionResult<User>> Delete(string name)
        {
            var foundUser = userService.DeleteUserByName(name);
            return Ok(foundUser);
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return userService.GetUserByName(name);
        }
    }
}