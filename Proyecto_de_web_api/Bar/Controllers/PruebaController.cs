using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                mensaje = "Mi Web API funciona correctamente",
                estado = "Conectado",
                proyecto = "JBar"
            });
        }
    }
}
