using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfiliacionServicios.Models;
using AfiliacionServicios.Models.ViewModels;

namespace AfiliacionServicios.Data.Services
{
    public interface ISolicitudAfiliacionService
    {
        void Delete(int? id);
        void Edit(SolicitudAfiliacion solicitudAfiliacion);
        void Create(SolicitudAfiliacion solicitudAfiliacion);
        List<SolicitudAfiliacion> Get();
        List<SolicitudAfiliacionVm> GetSolicitudesSinAprobar();
        
        Task<List<SolicitudAfiliacion>> GetSolicitudesAprobadas();
        Task<List<SolicitudAfiliacion>> GetSolicitudesActivas();
        Task<SolicitudAfiliacion> GetDetails(int? id);
        void Activacion(SolicitudAfiliacion solicitudAfiliacion);
        void Aprobacion(SolicitudAfiliacion solicitudAfiliacion);
        Task<SolicitudAfiliacion> AfiliacionNoAprobada(SolicitudAfiliacion solicitudAfiliacion);

        bool Exist(int id);
    }
}