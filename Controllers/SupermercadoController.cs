using Microsoft.AspNetCore.Mvc;
using Parcial2DDA.Models;
using Parcial2DDA.Services;

namespace Parcial2DDA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupermercadoController : ControllerBase
    {
        private readonly SupermercadoService _service;

        public SupermercadoController(SupermercadoService service)
        {
            _service = service;
        }
        
        // Post: /medicion
        [HttpPost("medicion")]
        public async Task<IActionResult> Medicion([FromBody] MedicionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.ProcesarMedicion(dto);

            return Ok(new
            {
                mensaje = "Medicion procesada correctamente"
            });
        }
        
        [HttpGet("reportes/total")]
        public async Task<IActionResult> ObtenerTotalMedicionesCompletadas()
        {
            var total = await _service.ReporteTotalMediciones();

            return Ok(new
            {
                total_mediciones_completadas = total
            });
        }
        
        [HttpGet("reportes/maxima_diferencia_peso")]
        public async Task<IActionResult> ObtenerMaximaDiferenciaPeso()
        {
            var entrada = await _service.ObtenerMedicionEntradaMaxima();
            var salida = await _service.ObtenerMedicionSalidaMaxima();
            
            var maximaDiferenciaPeso = _service.ReporteMaximaDiferenciaPeso(entrada, salida);

            return Ok(new
            {
                maxima_diferencia_peso = maximaDiferenciaPeso
            });
        }
        
        [HttpGet("reportes/maximo_tiempo_local")]
        public async Task<IActionResult> ObtenerMaximoTiempoEnLocal()
        {
            var entrada = await _service.ObtenerMedicionEntradaMaxima();
            var salida = await _service.ObtenerMedicionSalidaMaxima();

            if (entrada == null || salida == null)
            {
                return NotFound(new
                {
                    mensaje = "No hay suficientes mediciones para calcular el tiempo."
                });
            }

            var tiempo = _service.CalcularTiempoEnLocal(entrada, salida);

            return Ok(new
            {
                maximo_tiempo = tiempo
            });
        }
    }
}
