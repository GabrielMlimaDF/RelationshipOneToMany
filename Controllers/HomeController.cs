using Microsoft.AspNetCore.Mvc;
using Relação1N.Data;

namespace Relação1N.Controller
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult Get()
        {
            return Ok("Funcionando");
        }
    }
}