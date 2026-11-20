using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KardexController : ControllerBase
    {
        private readonly KardexService _services;

        public KardexController(KardexService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> ListarKardex()
        {
            try
            {
                var li = await _services.ListarKardex();
                return Ok(li);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("NuevoKardex")]
        public async Task<IActionResult> NuevoKardex(KardexDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                await _services.NuevoKardex(dTos);
                return StatusCode(201, "Kardex creado correctamente");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPut("editarKardex/{id}")]
        public async Task<IActionResult> EditarKardex(int id,[FromBody] KardexDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                if (id != dTos.id_kardex)
                {
                    return BadRequest(new { Message = "El id no coincide" });
                }
                dTos.id_kardex = id;
                await _services.EditarKardex(dTos);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpDelete("eliminarKardex/{id}")]
        public async Task<IActionResult> EliminarKardex(int id)
        {
           
                await _services.EliminarKardex(id);
                return NoContent();
        }
    }
}
