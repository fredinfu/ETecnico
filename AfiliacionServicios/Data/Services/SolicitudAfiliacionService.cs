using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfiliacionServicios.Models;
using AfiliacionServicios.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AfiliacionServicios.Data.Services
{
    public class SolicitudAfiliacionService : ISolicitudAfiliacionService
    {
        private readonly DataContext _context;
        public SolicitudAfiliacionService(DataContext context)
        {
            _context = context;
        }

        public async void Create(SolicitudAfiliacion solicitudAfiliacion)
        {
            _context.Add(solicitudAfiliacion);
            await _context.SaveChangesAsync();
        }
        public List<SolicitudAfiliacion> Get() => _context.SolicitudAfiliacion.ToList();

        public async Task<List<SolicitudAfiliacion>> GetSolicitudesAprobadas() => await _context.SolicitudAfiliacion.Where(m => m.Anulado == false && m.Aprobado).ToListAsync();        

        public async Task<List<SolicitudAfiliacion>> GetSolicitudesActivas() => await _context.SolicitudAfiliacion.Where(m => m.Anulado == false && m.Activado).ToListAsync();  
              
        public List<SolicitudAfiliacionVm> GetSolicitudesSinAprobar() {
            var res = _context.SolicitudAfiliacion.Where(m => m.Anulado == false).ToList();

            var vm = res
                .Select(s => new SolicitudAfiliacionVm{
                    id = s.id,
                    Servicio = s.Servicio,
                    NumeroIdentidad = s.NumeroIdentidad,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Aprobado = s.Aprobado,
                    Activado = s.Activado,
                    FechaCreacion = s.FechaCreacion,
                    DaysDiff = (DateTime.Today - s.FechaCreacion).Days 
                }).Where(m => m.Anulado == false && m.Aprobado == false && m.DaysDiff > 0).ToList();

            return vm;
        }

        public async Task<SolicitudAfiliacion> GetDetails(int? id)
        {
            
            return await _context.SolicitudAfiliacion
                .FirstOrDefaultAsync(m => m.id == id);
        }

        

        public async void Activacion(SolicitudAfiliacion solicitudAfiliacion)
        {
            solicitudAfiliacion.Activado = true;
            _context.Update(solicitudAfiliacion);
            await _context.SaveChangesAsync();
        }

        public async void Aprobacion(SolicitudAfiliacion solicitudAfiliacion)
        {
            solicitudAfiliacion.Aprobado = true;
            _context.Update(solicitudAfiliacion);
            await _context.SaveChangesAsync();
        }

        public async Task<SolicitudAfiliacion> AfiliacionNoAprobada(SolicitudAfiliacion solicitudAfiliacion)
        {
            var solicitudAfiliacionNoAprobada = await _context.SolicitudAfiliacion
                    .FirstOrDefaultAsync(m => m.NumeroIdentidad == solicitudAfiliacion.NumeroIdentidad && m.Aprobado == false && m.Anulado == false);
            
            return solicitudAfiliacionNoAprobada;
        }

        public async void Edit(SolicitudAfiliacion solicitudAfiliacion)
        {
            _context.Update(solicitudAfiliacion);
            await _context.SaveChangesAsync();
        }

        public void Delete(int? id)
        {
            var solicitudAfiliacion = _context.SolicitudAfiliacion
                .FirstOrDefault(m => m.id == id);

            solicitudAfiliacion.Anulado = true;
            _context.SaveChanges();
        }

        public bool Exist(int id)
        {
            return _context.SolicitudAfiliacion.Any(e => e.id == id);
        }
    }
}