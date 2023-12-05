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
        [HttpGet("GetUsers")]
        public ActionResult<List<User>> GetUsers()
        {
            if (_users != null && _users.Count > 0)
            {
                return Ok(_users);
            }
            return NotFound();

        }
        // Action Result
        //[HttpGet("GetUser/{id}")]
        //public ActionResult<User> GetUser(int id)
        //{
        //    User? user = _users.FirstOrDefault(x => x.Id.Equals(id));
        //    if (user != null)
        //    {
        //        return Ok(user);
        //    }
        //    return NotFound();
        //}

        // IActionResult - Interface is more flexible
        [HttpGet("GetUser/{id:int}")]
        public IActionResult GetUser(int id)
        {
            User? user = _users.FirstOrDefault(x => x.Id.Equals(id));
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            user.Id = _users.Max(x => x.Id) + 1;
            _users.Add(user);
            return Ok(user);

        }

        [HttpPut("{id:int}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            User? currentUsser = _users.FirstOrDefault(x => x.Id.Equals(id));
            if (currentUsser != null)
            {
                currentUsser.Name = user.Name;
                currentUsser.Email = user.Email;
                currentUsser.EmailConfirmed = user.EmailConfirmed;
                currentUsser.Password = user.Password;

                return Ok(currentUsser);

            }
            return NotFound();

        }

        [HttpDelete("{id:int}")]
        public ActionResult<User> Delete(int id)
        {
            User? userToDelete = _users.FirstOrDefault(x => x.Id.Equals(id));
            if (userToDelete != null)
            {
                _users.Remove(userToDelete);

                return Ok(new User());

            }
            return NotFound();

        }
    }
}
