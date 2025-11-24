using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Remitee.Challenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "pong" });
        }
    }
}
