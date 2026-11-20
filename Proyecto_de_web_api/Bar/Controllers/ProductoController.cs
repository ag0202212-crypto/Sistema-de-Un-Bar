using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _services;

        public ProductoController(ProductoService services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> ListarProductos()
        {
            try
            {
                var li = await _services.ListarProducto();
                return Ok(li);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("NuevoProducto")]
        public async Task<IActionResult> NuevoProducto(ProductoDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                await _services.NuevoProducto(dTos);
                return StatusCode(201, "Producto creado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPut("editarProducto/{id}")]
        public async Task<IActionResult> EditarProducto(int id, [FromBody] ProductoDTos dTos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                if (id != dTos.id_producto)
                {
                    return BadRequest(new { Message = "Id no valido" });
                }
                dTos.id_producto = id;
                await _services.EditarProducto(dTos);
                return Ok("Producto actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpDelete("eliminarProducto/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
                await _services.EliminarProducto(id);
                return Ok("Producto eliminado correctamente");
        }
    }
}
