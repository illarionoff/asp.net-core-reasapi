using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers
{
    public class TestController: Controller
    {
        [HttpGet("api/user")]
        public IActionResult Get()
        {
            return Ok(new {name = "alex"});
        }
    }
}