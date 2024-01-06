using Microsoft.AspNetCore.Mvc;
using REST.Database.Models;
using REST.Database.Services;

namespace REST.API._2.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
                _userService = userService;                
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {
            _userService.PostUser(user);
        }

        [HttpPut]
        public void Put([FromBody] User user)
        {
            var userToBeUpdated = _userService.GetUserById(user.Id);  
            if (userToBeUpdated != null)
            {
                _userService.PutUser(user);
            }
        }

        [HttpDelete]
        public void Delete([FromBody] User user)
        {
            var userToBeUpdated = _userService.GetUserById(user.Id);
            if (userToBeUpdated != null)
            {
                _userService.DeleteUser(user.Id);
            }
        }
    }
}
