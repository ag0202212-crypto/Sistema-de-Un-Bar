using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly CategoriaServices _services;


        public CategoriaController(CategoriaServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCategoria()
        {

            var li = await _services.Listarcategoria();
            return Ok(li);
        }
    }
}
