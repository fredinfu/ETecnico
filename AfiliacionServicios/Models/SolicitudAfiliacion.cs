using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfiliacionServicios.Models
{
    public class SolicitudAfiliacion
    {
        public int id { get; set; }
        [Display( Name="No. Identidad")]
        public string NumeroIdentidad { get; set; } 
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        [NotMapped]
        public string Afiliado => $"{Nombres} {Apellidos}";
        [DataType(DataType.Date)]
        [Display( Name="Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        public char Sexo { get; set; }     
        public string Servicio { get; set; }
        public bool Aprobado { get; set; } 
        public bool Activado { get; set; } 
        public bool Anulado { get; set; }
        public int IdUsuario { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        public SolicitudAfiliacion()
        {
            FechaCreacion = DateTime.Now;
        }
    }
}