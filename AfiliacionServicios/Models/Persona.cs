using System.ComponentModel.DataAnnotations;

namespace AfiliacionServicios.Models
{
    public class Persona
    {
        [Key]
        [Display(Name="No. Identidad")]
        public string NumeroIdentidad { get; set; } 
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        public char Sexo { get; set; }     


       
    }
}