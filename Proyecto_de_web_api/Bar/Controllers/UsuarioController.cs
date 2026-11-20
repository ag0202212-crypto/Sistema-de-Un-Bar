using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _services;

        [HttpGet]
        public async Task<IActionResult> ListarUsuario()
        {
            var olist = await _services.ListarUsuario();
            return Ok(olist);
        }

        [HttpPost]
        public async Task<IActionResult> NuevooUsuario(UsuarioDTos dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El Modelo no es Valido" });
                }
                await _services.NuevoUsuario(dto);
                return StatusCode(201, "Usuario agregado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarUsuario(int id, [FromBody] UsuarioDTos dtos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El modelo no es Valido" });
                }
                if (id != dtos.id_usuario)
                {
                    return BadRequest(new { msj = "El ID no es Valido" });
                }
                dtos.id_usuario = id;
                await _services.EditarUsuario(dtos);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
    }
}
