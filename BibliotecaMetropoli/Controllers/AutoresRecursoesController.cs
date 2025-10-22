using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaMetropoli.Data;
using BibliotecaMetropoli.Models;

namespace BibliotecaMetropoli.Controllers
{
    public class AutoresRecursoesController : Controller
    {
        private readonly BibliotecaMetropoliContext _context;

        public AutoresRecursoesController(BibliotecaMetropoliContext context)
        {
            _context = context;
        }

        // GET: AutoresRecursoes
        public async Task<IActionResult> Index()
        {
            var bibliotecaMetropoliContext = _context.AutoresRecursos
                .Include(a => a.Autor)
                .Include(a => a.Recurso);
            return View(await bibliotecaMetropoliContext.ToListAsync());
        }

        // GET: AutoresRecursoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar por IdRec (como clave primaria simple temporalmente)
            var autoresRecurso = await _context.AutoresRecursos
                .Include(a => a.Autor)
                .Include(a => a.Recurso)
                .FirstOrDefaultAsync(m => m.IdRec == id);

            if (autoresRecurso == null)
            {
                return NotFound();
            }

            return View(autoresRecurso);
        }

        // GET: AutoresRecursoes/Create
        public IActionResult Create()
        {
            ViewData["idAutor"] = new SelectList(_context.Autores, "idAutor", "Apellidos");
            ViewData["IdRec"] = new SelectList(_context.Recursos, "IdRec", "Titulo");
            return View();
        }

        // POST: AutoresRecursoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRec,idAutor,EsPrincipal")] AutoresRecurso autoresRecurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autoresRecurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idAutor"] = new SelectList(_context.Autores, "idAutor", "Apellidos", autoresRecurso.idAutor);
            ViewData["IdRec"] = new SelectList(_context.Recursos, "IdRec", "Titulo", autoresRecurso.IdRec);
            return View(autoresRecurso);
        }

        // GET: AutoresRecursoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar por IdRec (como clave primaria simple temporalmente)
            var autoresRecurso = await _context.AutoresRecursos
                .Include(a => a.Autor)
                .Include(a => a.Recurso)
                .FirstOrDefaultAsync(m => m.IdRec == id);

            if (autoresRecurso == null)
            {
                return NotFound();
            }

            ViewData["idAutor"] = new SelectList(_context.Autores, "idAutor", "Apellidos", autoresRecurso.idAutor);
            ViewData["IdRec"] = new SelectList(_context.Recursos, "IdRec", "Titulo", autoresRecurso.IdRec);
            return View(autoresRecurso);
        }

        // POST: AutoresRecursoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRec,idAutor,EsPrincipal")] AutoresRecurso autoresRecurso)
        {
            if (id != autoresRecurso.IdRec)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autoresRecurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoresRecursoExists(autoresRecurso.IdRec))
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
            ViewData["idAutor"] = new SelectList(_context.Autores, "idAutor", "Apellidos", autoresRecurso.idAutor);
            ViewData["IdRec"] = new SelectList(_context.Recursos, "IdRec", "Titulo", autoresRecurso.IdRec);
            return View(autoresRecurso);
        }

        // GET: AutoresRecursoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar por IdRec (como clave primaria simple temporalmente)
            var autoresRecurso = await _context.AutoresRecursos
                .Include(a => a.Autor)
                .Include(a => a.Recurso)
                .FirstOrDefaultAsync(m => m.IdRec == id);

            if (autoresRecurso == null)
            {
                return NotFound();
            }

            return View(autoresRecurso);
        }

        // POST: AutoresRecursoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Buscar por IdRec (como clave primaria simple temporalmente)
            var autoresRecurso = await _context.AutoresRecursos
                .FirstOrDefaultAsync(m => m.IdRec == id);

            if (autoresRecurso != null)
            {
                _context.AutoresRecursos.Remove(autoresRecurso);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AutoresRecursoExists(int id)
        {
            return _context.AutoresRecursos.Any(e => e.IdRec == id);
        }
    }
}