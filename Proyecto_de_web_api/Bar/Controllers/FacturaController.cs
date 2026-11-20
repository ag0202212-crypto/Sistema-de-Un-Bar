using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaService _services;

        public FacturaController(FacturaService services)
        {
            _services = services;
        }


        //listar
        [HttpGet]
        public async Task<IActionResult> ListarFactura()
        {
            try
            {
                var lista = await _services.ListarFactura();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del seridor" + ex.Message);
            }
        }

        //insertar
        [HttpPost("Nueva Factura")]
        public async Task<IActionResult> NuevaFactura(FacturaDTos Dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El modelo no es valido" });
                }
                await _services.NuevoFactura(Dto);
                return StatusCode(201, "Fatura Agregada exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor" + ex.Message);
            }
        }

        //editar
        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarFactura(int id, [FromBody] FacturaDTos dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El modelo no es valido " });
                }
                if (id != dto.id_factura)
                {
                    return BadRequest(new { msj = "El ID no Coincicde " });
                }
                dto.id_factura = id;
                await _services.EditarFactura(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor" + ex.Message);
            }
        }


        //Eliminar
        [HttpDelete("Elimar/{id}")]
        public async Task<IActionResult> EliminarFactura(int id)
        {
            await _services.EliminarFactura(id);
            return NoContent();
        }
    }
}
