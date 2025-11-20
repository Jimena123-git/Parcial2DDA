using System.ComponentModel.DataAnnotations;

namespace Parcial2DDA.Models
{
    public class Medicion
    {
        [Key]
        public int Id { get; set; }
        public string Huella { get; set; }
        public decimal Peso { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
