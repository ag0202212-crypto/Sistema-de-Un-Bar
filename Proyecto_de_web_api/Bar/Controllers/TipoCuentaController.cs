using Application.DTos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCuentaController : ControllerBase
    {
        private readonly Tipo_de_CuentaService _service;

        public TipoCuentaController(Tipo_de_CuentaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTipoCuenta()
        {
            var olist = await _service.ListarTipo_de_Cuenta();
            return Ok(olist);
        }

        [HttpPost]
        public async Task<IActionResult> NuevoTipoCuenta(Tipo_de_CuentaDTos dtos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El Modelo no es Valido" });
                }
                await _service.NuevoTipo_de_Cuenta(dtos);
                return StatusCode(201, "Tipo de Cuenta agregada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarTipoCuenta(int id, [FromBody] Tipo_de_CuentaDTos dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "El Modelo no es Valido" });

                }
                if (id != dto.id_tipo_cuenta)
                {
                    return BadRequest(new { msj = "El ID no coincide" });
                }
                dto.id_tipo_cuenta = id;
                await _service.EditarTipo_de_Cuenta(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
    }
}
