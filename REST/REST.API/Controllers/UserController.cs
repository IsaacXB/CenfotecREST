using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST.API.Models;

namespace REST.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(new List<User> { 
                new User { 
                    Id = 1, 
                    Name = "Test",
                    Email = "test@gmail.com",
                    EmailConfirmed = "No",
                    Password = "abc123$."
                    
                } 
            });
        }
    }
}
