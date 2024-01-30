using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using REST.Database.Models;
using REST.Database.Services;
using System.Security.Claims;

namespace REST.API._2.Controllers
{
    //[EnableCors("Policy1")]
    [Route("api/users")]
    [ApiController]
    //[Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
                _userService = userService;                
        }

        //[DisableCors]
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_userService.GetUsers());
        }

        //[HttpGet("{id}")]
        //public IActionResult Index(int id)
        //{
        //    var user = _userService.GetUserById(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}


        /// <summary>
        /// Get user data by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> IndexAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //[HttpPost]
        //public void Post([FromBody] User user)
        //{
        //    _userService.PostUser(user);
        //}

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="user">User Information</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /User
        ///     {
        ///         "Name": "David",
        ///         "Email": "David@gmail.com",
        ///         "Password": "1234@",
        ///         "EmailConfirmed": "No"
        ///     }
        /// </remarks>
        /// <response code="200">User created successfully</response>
        /// <response code="400">An error has ocurred, user not created.</response>

        [HttpPost]
        public async Task PostAsync([FromBody] User user)
        {
            await _userService.PostUserAsync(user);
        }

        //[HttpPut]
        //public void Put([FromBody] User user)
        //{
        //    var userToBeUpdated = _userService.GetUserById(user.Id);  
        //    if (userToBeUpdated != null)
        //    {
        //        _userService.PutUser(user);
        //    }
        //}

        //[HttpPut]
        //public async Task PutAsync([FromBody] User user)
        //{
        //    var userToBeUpdated = await _userService.GetUserByIdAsync(user.Id);
        //    if (userToBeUpdated != null)
        //    {
        //        await _userService.PutUserAsync(user);
        //    }
        //}


        /// <summary>
        /// Updates User Information
        /// </summary>
        /// <param name="id">User Id to be updated</param>
        /// <param name="user">Model Instance</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] User user)
        {
            var userToBeUpdated = await _userService.GetUserByIdAsync(user.Id);
            if (userToBeUpdated != null)
            {
                await _userService.PutUserAsync(user);
            }
        }

        //[HttpDelete]
        //public void Delete([FromBody] User user)
        //{
        //    var userToBeUpdated = _userService.GetUserById(user.Id);
        //    if (userToBeUpdated != null)
        //    {
        //        _userService.DeleteUser(user.Id);
        //    }
        //}

        //[EnableCors("Policy1")]
        //[HttpDelete]
        //public async Task DeleteAsync([FromBody] User user)
        //{
        //    var userToBeUpdated = await _userService.GetUserByIdAsync(user.Id);
        //    if (userToBeUpdated != null)
        //    {
        //        await _userService.DeleteUserAsync(user.Id);
        //    }
        //}

        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="id">User Id to be deleted</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            var userToBeUpdated = await _userService.GetUserByIdAsync(id);
            if (userToBeUpdated != null)
            {
                await _userService.DeleteUserAsync(id);
            }
        }


        /// <summary>
        /// Returns an error exception
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Exception Details</exception>
        //[EnableCors("Policy2")]
        [EnableCors("Policy1")]
        [HttpGet("ReturnError")]
        public IActionResult ReturnError() 
        {
            return new JsonResult("An error has occurred, please contact your system administrator.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        [Route("/ErrorDevelopment")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ErrorHandlerDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message
                );
        }

        [Route("/Error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ErrorHandler()
        {
            return Problem();
        }


        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] AuthRequest data)
        {

            var userResponse = _userService.Authenticate(data);

            if (userResponse == null)
            {
                return BadRequest("Incorrect credentials.User name and password combination are not valid.");
            }

            return Ok(userResponse);

        }

        /// <summary>
        /// Gets user authentication claims - Requires authorization
        /// </summary>
        /// <returns></returns>
        [HttpGet("Auth")]
        [Authorize]
        public ActionResult GetAuth()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                // Lista los valores
                IEnumerable<Claim> claims = identity.Claims;

                var claim = identity.FindFirst(
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (claim != null)
                {
                    return new JsonResult(claim.Value);
                }
            }

            return new JsonResult("Unauthorized")
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };

        }




    }
}
