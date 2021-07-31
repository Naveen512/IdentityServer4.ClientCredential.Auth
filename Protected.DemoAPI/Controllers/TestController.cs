using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Protected.DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("case1")]
        [Authorize]
        public IActionResult Case1()
        {
            return Ok("Hi simple authorization enabled");
        }

        [HttpGet]
        [Route("case2")]
        [Authorize(Policy = "PolicyScope1")]
        public IActionResult Case2()
        {
            return Ok("Hi policy base authorization enabled");
        }

        [HttpGet]
        [Route("case3")]
        [Authorize(Policy = "PolicyScope2")]
        public IActionResult Case3()
        {
            return Ok("Hi policy base authorization enabled");
        }

    }
}
