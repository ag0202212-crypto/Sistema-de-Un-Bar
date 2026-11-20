using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleComandaController : ControllerBase
    {
        private readonly Detalle_ComandaService _services;

        public DetalleComandaController(Detalle_ComandaService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> ListarDetalleComanda()
        {
            try
            {
                var li = await _services.ListarDetalle_Comanda();
                return Ok(li);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("Nuevo Detalle Comanda")]
        public async Task<IActionResult> NuevoDetalleComanda(Detalle_ComandaDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                await _services.NuevoDetalle_Comanda(dTos);
                return StatusCode(201, "Kardex creado correctamente");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> EditarDetalleComanda(int id, [FromBody] Detalle_ComandaDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                if (id != dTos.id_detalle_comanda)
                {
                    return BadRequest(new { Message = "El id no coincide" });
                }
                dTos.id_detalle_comanda = id;
                await _services.EditarDetalle_Comanda(dTos);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> ELiminarDetalleComanda(int id)
        {

            await _services.EliminarDetalle_Comanda(id);
            return NoContent();
        }
    }
}
