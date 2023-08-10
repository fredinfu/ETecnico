using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AfiliacionServicios.Data;
using AfiliacionServicios.Models;

namespace AfiliacionServicios.Controllers
{
    public class SolicitudAfiliacionController : Controller
    {
        private readonly DataContext _context;

        public SolicitudAfiliacionController(DataContext context)
        {
            _context = context;
        }

        // GET: SolicitudAfiliacion
        public async Task<IActionResult> Index()
        {
            return View(await _context.SolicitudAfiliacion.Where(m => m.Anulado == false).ToListAsync());
        }

        // GET: SolicitudAfiliacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudAfiliacion = await _context.SolicitudAfiliacion
                .FirstOrDefaultAsync(m => m.id == id);
            if (solicitudAfiliacion == null)
            {
                return NotFound();
            }

            return View(solicitudAfiliacion);
        }
        // GET: SolicitudAfiliacion/Activacion/1
        public async Task<IActionResult> Activacion(int? id){
            if (id == null) return NotFound();

            var solicitudAfiliacion = await _context.SolicitudAfiliacion.FirstOrDefaultAsync(m => m.id == id && m.Anulado == false);

            if(solicitudAfiliacion == null) return NotFound();

            return View(solicitudAfiliacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activacion(int? id, [Bind("id,NumeroIdentidad,Nombres,Apellidos,FechaNacimiento,Sexo,Aprobado,Activado,Anulado,FechaCreacion,Servicio")] SolicitudAfiliacion solicitudAfiliacion){
            if(id != solicitudAfiliacion.id) return NotFound();

            try {
                solicitudAfiliacion.Activado = true;
                _context.Update(solicitudAfiliacion);
                await _context.SaveChangesAsync();
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

            var solicitudAfiliacion = await _context.SolicitudAfiliacion
                .FirstOrDefaultAsync(m => m.id == id && m.Anulado == false);
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
                    solicitudAfiliacion.Aprobado = true;
                    _context.Update(solicitudAfiliacion);
                    await _context.SaveChangesAsync();
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
                

                var solicitudAfiliacionNoAprobada = await _context.SolicitudAfiliacion
                    .FirstOrDefaultAsync(m => m.NumeroIdentidad == solicitudAfiliacion.NumeroIdentidad && m.Aprobado == false && m.Anulado == false);

                if(solicitudAfiliacionNoAprobada != null) {
                    ViewBag.Message = $"Ya existen solicitudes sin aprobar para el afiliado, favor revisar que esten aprobada las afiliaciones del clientes antes de crear otra";
                    return View();
                }                

                solicitudAfiliacion.FechaCreacion = DateTime.Now;
                _context.Add(solicitudAfiliacion);
                await _context.SaveChangesAsync();
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

            var solicitudAfiliacion = await _context.SolicitudAfiliacion.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,NumeroIdentidad,Nombres,Apellidos,FechaNacimiento,Sexo,Aprobado,Activado,Anulado,FechaCreacion")] SolicitudAfiliacion solicitudAfiliacion)
        {
            if (id != solicitudAfiliacion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudAfiliacion);
                    await _context.SaveChangesAsync();
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
            return View(solicitudAfiliacion);
        }

        // GET: SolicitudAfiliacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudAfiliacion = await _context.SolicitudAfiliacion
                .FirstOrDefaultAsync(m => m.id == id);
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
            var solicitudAfiliacion = await _context.SolicitudAfiliacion.FindAsync(id);
            if(solicitudAfiliacion == null) return NotFound();

            solicitudAfiliacion.Anulado = true;
            // _context.SolicitudAfiliacion.Remove(solicitudAfiliacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudAfiliacionExists(int id)
        {
            return _context.SolicitudAfiliacion.Any(e => e.id == id);
        }
    }
}
