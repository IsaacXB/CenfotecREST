using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST.API.Models;

namespace REST.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private List<User> _users = new List<User>() 
        {
            new User {
                    Id = 1,
                    Name = "José",
                    Email = "jvdg@gmail.com",
                    EmailConfirmed = "No",
                    Password = "abc123$."
                },
            new User {
                    Id = 2,
                    Name = "Rebeca",
                    Email = "rebeca@gmail.com",
                    EmailConfirmed = "No",
                    Password = "abc123$."
                },
        };
        [HttpGet]
        public List<User> GetUsers()
        {
            return _users;
        }
    }
}
