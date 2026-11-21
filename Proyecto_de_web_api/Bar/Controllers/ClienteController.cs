using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteServices _services;

        public ClienteController (ClienteServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCliente()
        {
            try
            {
                var list =await _services.Listarcliente();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Internal Server Error"+ ex.Message);
            }
        }

        [HttpPost ("Nuevo Cliente")]
        public async Task<IActionResult> NuevoCliente(ClienteDTos dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Mesagge = "No es valido" });
                }
                await _services.NuevoCliente(dto);
                return StatusCode(200, "Nuevo Cliente Creado Correctamente");
            }
            catch(Exception ex)
            {
                return StatusCode(500,"Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarCliente(int id, [FromBody] ClienteDTos dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return BadRequest(new { Message = "No es valido" });
                }
                if (id != dto.id_cliente)
                {
                    return BadRequest(new { Messge= "El id no coincide"});
                }
                dto.id_cliente = id;
                await _services.EditarCliente(dto);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            await _services.EliminarCliente(id);
            return NoContent();
        }
    }
}
