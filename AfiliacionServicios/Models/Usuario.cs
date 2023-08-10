
namespace AfiliacionServicios.Models
{
    public class Usuario 
    {
        public int Id { get; set; } 
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreUsuario { get; set; }
        public int Acceso { get; set; } 
        public bool Activo { get; set; }
        
    }
}