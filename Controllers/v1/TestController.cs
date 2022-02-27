using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v1
{
<<<<<<< HEAD
    [ApiVersion("1.0")] // Query versioning for example https://localhost:44359/api/test/get-test-data?api-version=1.0
    [ApiVersion("1.1")]
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")] // Url versioning for example https://localhost:44359/api/v1/test/get-test-data
=======
    //[ApiVersion("1.0")] // Query versioning for example https://localhost:44359/api/test/get-test-data?api-version=1.0
    //[ApiVersion("1.1")]
    [ApiController]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")] // Url versioning for example https://localhost:44359/api/v1/test/get-test-data
>>>>>>> logging
    public class TestController:ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("This is a Test controller V1");
        }

<<<<<<< HEAD
        [HttpGet("get-test-data")]
        [MapToApiVersion("1.1")]
        public IActionResult Getv11()
        {
            return Ok("This is a Test controller V1.1");
        }
=======
        //[HttpGet("get-test-data")]
        //[MapToApiVersion("1.1")]
        //public IActionResult Getv11()
        //{
        //    return Ok("This is a Test controller V1.1");
        //}
>>>>>>> logging
    }
}
