using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using REST.Database.Models;
using REST.Database.Services;

namespace REST.Homework2.Controllers.V1
{
    [ApiVersion("1.0")]
    [EnableCors("Policy1")]
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
        public async Task<IActionResult> IndexAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task PostAsync([FromBody] User user)
        {
            await _userService.PostUserAsync(user);
        }

        [HttpPut]
        public async Task PutAsync([FromBody] User user)
        {
            var userToBeUpdated = await _userService.GetUserByIdAsync(user.Id);
            if (userToBeUpdated != null)
            {
                await _userService.PutUserAsync(user);
            }
        }

        [HttpDelete]
        public async Task DeleteAsync([FromBody] User user)
        {
            var userToBeUpdated = await _userService.GetUserByIdAsync(user.Id);
            if (userToBeUpdated != null)
            {
                await _userService.DeleteUserAsync(user.Id);
            }
        }

        [HttpGet("ReturnError")]
        public IActionResult ReturnError()
        {
            throw new Exception("Exception Handling Example V1");
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

    }
}
