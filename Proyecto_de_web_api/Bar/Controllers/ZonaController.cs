using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonaController : ControllerBase
    {
        private readonly ZonaService _service;

        public ZonaController(ZonaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarZona()
        {
            var olist = await _service.ListarZona();
            return Ok(olist);
        }

        [HttpPost]
        public async Task<IActionResult> NuevaZona(ZonaDTos dtos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El Modelo no es Valido" });
                }
                await _service.NuevoZona(dtos);
                return StatusCode(201, "Zona agregada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarZona(int id, [FromBody] ZonaDTos dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El Modelo no es Valido" });

                }
                if (id != dto.id_zona)
                {
                    return BadRequest(new { msj = "El ID no coincide" });
                }
                dto.id_zona = id;
                await _service.EditarZona(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
    }
}
