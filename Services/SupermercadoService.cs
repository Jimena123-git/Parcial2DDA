using Microsoft.EntityFrameworkCore;
using Parcial2DDA.Data;
using Parcial2DDA.Models;

namespace Parcial2DDA.Services
{
    public class SupermercadoService
    {
        private readonly AppDbContext _dbcontext;

        public SupermercadoService(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task ProcesarMedicion(MedicionDto dto)
        {
            var medicion = new Medicion
            {
                Huella = dto.Huella,
                Peso = dto.Peso,
                Tipo = dto.Tipo,
                FechaHora = DateTime.Now
            };

            _dbcontext.Mediciones.Add(medicion);
            await _dbcontext.SaveChangesAsync();
        }
       
        public TimeSpan CalcularTiempoEnLocal(Medicion entrada, Medicion salida)
        {
            return salida.FechaHora - entrada.FechaHora;
        }
        public async Task BorrarMediciones(Medicion entrada, Medicion salida)
        {
            _dbcontext.Mediciones.Remove(entrada);
            _dbcontext.Mediciones.Remove(salida);

            await _dbcontext.SaveChangesAsync();
        }
        public async Task<int> ReporteTotalMediciones()
        {
            return await _dbcontext.Mediciones.CountAsync();
        }
        public async Task<Medicion> ObtenerMedicionEntradaMaxima()
        {
            return await _dbcontext.Mediciones
                .Where(m => m.Tipo == "Entrada")
                .OrderBy(m => m.FechaHora)
                .FirstOrDefaultAsync();
        }
        public async Task<Medicion> ObtenerMedicionSalidaMaxima()
        {
            return await _dbcontext.Mediciones
                .Where(m => m.Tipo == "Salida")
                .OrderByDescending(m => m.FechaHora)
                .FirstOrDefaultAsync();
        }

        public double ReporteMaximaDiferenciaPeso(Medicion entrada, Medicion salida)
        {
            return (double)(salida.Peso - entrada.Peso);
        }
    }
}


