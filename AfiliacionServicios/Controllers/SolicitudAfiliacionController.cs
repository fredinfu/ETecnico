using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AfiliacionServicios.Data;
using AfiliacionServicios.Models;
using AfiliacionServicios.Models.ViewModels;
using AfiliacionServicios.Data.Services;

namespace AfiliacionServicios.Controllers
{
    public class SolicitudAfiliacionController : Controller
    {
        private readonly DataContext _context;
        private readonly ISolicitudAfiliacionService _service;

        public SolicitudAfiliacionController(ISolicitudAfiliacionService service)
        {
            _service = service;
        }

        // GET: SolicitudAfiliacion
        public async Task<IActionResult> Index()
        {
            var vm = _service.GetSolicitudesSinAprobar();
            return View(vm);
        }

        // GET: SolicitudAfiliacion/SolicitudesTodas
        public async Task<IActionResult> SolicitudesTodas()
        {      
            return View(_service.Get());
        }

        // GET: SolicitudAfiliacion/SolicitudesAprobadas
        public async Task<IActionResult> SolicitudesAprobadas()
        {           
            return View(await _service.GetSolicitudesAprobadas());
        }

        // GET: SolicitudAfiliacion/SolicitudesActivas
        public async Task<IActionResult> SolicitudesActivas()
        {
            var res = await _service.GetSolicitudesActivas();

           
            return View(res);
        }


        // GET: SolicitudAfiliacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudAfiliacion = await _service.GetDetails(id);
            if (solicitudAfiliacion == null)
            {
                return NotFound();
            }

            return View(solicitudAfiliacion);
        }
        // GET: SolicitudAfiliacion/Activacion/1
        public async Task<IActionResult> Activacion(int? id){
            if (id == null) return NotFound();

            var solicitudAfiliacion = await _service.GetDetails(id);
            if(solicitudAfiliacion == null) return NotFound();

            return View(solicitudAfiliacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activacion(int? id, [Bind("id,NumeroIdentidad,Nombres,Apellidos,FechaNacimiento,Sexo,Aprobado,Activado,Anulado,FechaCreacion,Servicio")] SolicitudAfiliacion solicitudAfiliacion){
            if(id != solicitudAfiliacion.id) return NotFound();

            try {
                _service.Activacion(solicitudAfiliacion);
            } catch (DbUpdateConcurrencyException ex) {
                if (!SolicitudAfiliacionExists(solicitudAfiliacion.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
                return RedirectToAction(nameof(Index));

        }

        // GET: SolicitudAfiliacion/Aprobacion/5
        public async Task<IActionResult> Aprobacion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudAfiliacion = await _service.GetDetails(id);
            if (solicitudAfiliacion == null)
            {
                return NotFound();
            }

            return View(solicitudAfiliacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aprobacion(int? id, [Bind("id,NumeroIdentidad,Nombres,Apellidos,FechaNacimiento,Sexo,Aprobado,Activado,Anulado,FechaCreacion,Servicio")] SolicitudAfiliacion solicitudAfiliacion)
        {
            
            if (id != solicitudAfiliacion.id)
            {
                return NotFound();
            }

                try
                {
                    _service.Aprobacion(solicitudAfiliacion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudAfiliacionExists(solicitudAfiliacion.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

        }

        // GET: SolicitudAfiliacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SolicitudAfiliacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,NumeroIdentidad,Nombres,Apellidos,Servicio,FechaNacimiento,Sexo")] SolicitudAfiliacion solicitudAfiliacion)
        {
            try {
                

                var solicitudAfiliacionNoAprobada = await _service.AfiliacionNoAprobada(solicitudAfiliacion);
                if(solicitudAfiliacionNoAprobada != null) {
                    ViewBag.Message = $"Ya existen solicitudes sin aprobar para el afiliado, favor revisar que esten aprobada las afiliaciones del clientes antes de crear otra";
                    return View();
                }                

                _service.Create(solicitudAfiliacion);
                return RedirectToAction(nameof(Index));

                
            } catch (Exception ex) { 
                return View(solicitudAfiliacion);
            }
            
        }

        // GET: SolicitudAfiliacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudAfiliacion = await _service.GetDetails(id);
            if (solicitudAfiliacion == null)
            {
                return NotFound();
            }
            return View(solicitudAfiliacion);
        }

        // POST: SolicitudAfiliacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,NumeroIdentidad,Nombres,Apellidos,FechaNacimiento,Sexo,Aprobado,Activado,Anulado,FechaCreacion,Servicio")] SolicitudAfiliacion solicitudAfiliacion)
        {
            if (id != solicitudAfiliacion.id)
            {
                return NotFound();
            }

                try
                {
                    _service.Edit(solicitudAfiliacion);
                    // _context.Update(solicitudAfiliacion);
                    // await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudAfiliacionExists(solicitudAfiliacion.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            //return View(solicitudAfiliacion);
        }

        // GET: SolicitudAfiliacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudAfiliacion = await _service.GetDetails(id);
            if (solicitudAfiliacion == null)
            {
                return NotFound();
            }

            return View(solicitudAfiliacion);
        }

        // POST: SolicitudAfiliacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitudAfiliacion = await _service.GetDetails(id);
            if(solicitudAfiliacion == null) return NotFound();

            _service.Delete(id);
            // solicitudAfiliacion.Anulado = true;
            // _context.SolicitudAfiliacion.Remove(solicitudAfiliacion);
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudAfiliacionExists(int id)
        {
            return _context.SolicitudAfiliacion.Any(e => e.id == id);
        }
    }
}
