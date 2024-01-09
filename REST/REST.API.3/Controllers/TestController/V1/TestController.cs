using Microsoft.AspNetCore.Mvc;

namespace REST.API._3.Controllers.TestController.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    //[ApiVersion("1.0", deprecated =true)]

    [Route("api/[Controller]")]
    //[Route("api/v{version:apiVersion}/[Controller]")]
    public class TestController : Controller
    {
        //[MapToApiVersion("1.0")]

        [HttpGet]
        //[Obsolete("Use version 2")]
        public IActionResult GetTest()
        {
            return Content("Hi from Test Controller: Version 1 (default)");
        }
    }
}
