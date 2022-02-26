using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v2
{
    [ApiVersion("2.0")] // Query versioning for example https://localhost:44359/api/test/get-test-data?api-version=2.0
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]  // Url versioning for example https://localhost:44359/api/v2/test/get-test-data
    public class TestController:ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("This is a Test controller V2");
        }
    }
}
