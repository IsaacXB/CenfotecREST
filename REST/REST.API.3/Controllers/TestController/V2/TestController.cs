using Microsoft.AspNetCore.Mvc;

namespace REST.API._3.Controllers.TestController.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[Controller]")]
    //[Route("api/v{version:apiVersion}/[Controller]")]

    public class TestController : Controller
    {
        //[MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetTest()
        {
            return Content("Hi from Test Controller: Version 2 (beta)");
        }
    }
}
