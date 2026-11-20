using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly DireccionService _services;

        public DireccionController(DireccionService services)
        {
            _services = services;
        }


        //listar
        [HttpGet]
        public async Task<IActionResult> ListarDireccion()
        {
            try
            {
                var lista = await _services.ListarDireccion();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del seridor" + ex.Message);
            }
        }

        //insertar
        [HttpPost("Nueva Direccion")]
        public async Task<IActionResult> NuevaDireccion(DireccionDTos Dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El modelo no es valido" });
                }
                await _services.NuevoDireccion(Dto);
                return StatusCode(201, "Direccion Agregada exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor" + ex.Message);
            }
        }

        //editar
        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarDireccion(int id, [FromBody] DireccionDTos dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El modelo no es valido " });
                }
                if (id != dto.id_direccion)
                {
                    return BadRequest(new { msj = "El ID no Coincicde " });
                }
                dto.id_direccion = id;
                await _services.EditarDireccion(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor" + ex.Message);
            }
        }


        //Eliminar
        [HttpDelete("Elimar/{id}")]
        public async Task<IActionResult> EliminarDireccion(int id)
        {
            await _services.EliminarDireccion(id);
            return NoContent();
        }
    }
}
