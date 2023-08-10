using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfiliacionServicios.Models.ViewModels
{
    public class SolicitudAfiliacionVm
    {
        public int id { get; set; }
        public string Servicio { get; set; }
        [Display( Name="No. Identidad")]
        public string NumeroIdentidad { get; set; } 
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        [NotMapped]
        public string Afiliado => $"{Nombres} {Apellidos}";
        public bool Aprobado { get; set; } 
        public bool Activado { get; set; } 
        public bool Anulado { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        public int DaysDiff { get; set; }
    }
}