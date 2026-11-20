using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> ListarServicios()
        {
            try
            {
                var li = await _services.Listarcategoria();
                return Ok(li);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("Nueva categoria")]
        public async Task<IActionResult> NuevoServicio(CategoriaDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                await _services.NuevoCategoria(dTos);
                return StatusCode(201, "Kardex creado correctamente");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> EditarCategoria(int id, [FromBody] CategoriaDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                if (id != dTos.id_categoria)
                {
                    return BadRequest(new { Message = "El id no coincide" });
                }
                dTos.id_categoria = id;
                await _services.EditarCategoria(dTos);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {

            await _services.EliminarCategoria(id);
            return NoContent();
        }
    }
}
