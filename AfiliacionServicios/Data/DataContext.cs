using AfiliacionServicios.Models;
using Microsoft.EntityFrameworkCore;

namespace AfiliacionServicios.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Usuario> Usuarios {get;set;}
        public DbSet<Persona> Personas { get; set; }
        public DbSet<SolicitudAfiliacion> SolicitudAfiliacion { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        

    }
}