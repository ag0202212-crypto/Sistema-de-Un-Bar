using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsientoContableController : ControllerBase
    {
        private readonly Asiento_ContableService _services;

        public AsientoContableController(Asiento_ContableService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsientoContable()
        {
            try
            {
                var list = await _services.ListarAsiento_Contable();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }

        }

        [HttpPost ("Nuevo Asiento Contable")]
        public async Task<IActionResult> NuevoAsientoContable( Asiento_ContableDTos dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                await _services.NuevoAsiento_Contable(dto);
                return StatusCode(201, "Asiento contable creado correctamente");

            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarAsientoContable(int id, [FromBody] Asiento_ContableDTos dto)
        {
            try
            {

           
            if (ModelState.IsValid)
            {

                return BadRequest(new { Message = "No es valido" });
            }
            if (id != dto.id_asiento_contable)
            {
                return BadRequest(new { Message = "El id no coincide" });
            }
            dto.id_asiento_contable = id;
                await _services.EditarAsiento_Contable(dto);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarAsientoContable(int id)
        {
            await _services.EliminarAsiento_Contable(id);
            return NoContent();
        }
       
    }
}
