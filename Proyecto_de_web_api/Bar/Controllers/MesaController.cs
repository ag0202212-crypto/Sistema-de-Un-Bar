using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly MesaService _services;

        public MesaController(MesaService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> ListarMesas()
        {
            try
            {
                var mesas = await _services.ListarMesa();
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("NuevaMesa")]
        public async Task<IActionResult> NuevaMesa(MesaDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                await _services.NuevoMesa(dTos);
                return StatusCode(201, "Mesa creada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPut("editarMesa/{id}")]
        public async Task<IActionResult> EditarMesa(int id, [FromBody] MesaDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                if (id != dTos.id_mesa)
                {
                    return BadRequest(new { Message = "Id no valido" });
                }
                dTos.id_mesa = id;
                await _services.EditarMesa(dTos);
                return Ok("Mesa actualizada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpDelete("eliminarMesa/{id}")]
        public async Task<IActionResult> EliminarMesa(int id)
        { 
                await _services.EliminarMesa(id);
                return Ok("Mesa eliminada correctamente");
        }
    }

}
