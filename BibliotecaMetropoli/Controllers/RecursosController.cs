using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaMetropoli.Data;
using BibliotecaMetropoli.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//integrantes
//Luis Enrique Cartagena Arteaga
//Elisabet Beatriz Marroquín González
//Verónica Elizabeth Rodríguez Majano
//Ever Gabriel Cabezas Alfaro
//Ricardo Daniel Alfaro Tomasino

namespace BibliotecaMetropoli.Controllers
{
    public class RecursosController : Controller
    {
        private readonly BibliotecaMetropoliContext _context;

        public RecursosController(BibliotecaMetropoliContext context)
        {
            _context = context;
        }

        // Método para mostrar todos los recursos
        public ActionResult Index()
        {
            var recursos = _context.Recursos
                .Include(r => r.AutoresRecursos)
                    .ThenInclude(ar => ar.Autor)
                .Include(r => r.Editorial)
                .Include(r => r.TipoRecurso)
                .Include(r => r.Pais)
                .ToList();

            return View(recursos);
        }

        // Método para mostrar el formulario de búsqueda
        public ActionResult Buscar()
        {
            // Cargar las listas para los dropdowns
            ViewBag.Editoriales = _context.Editoriales.ToList();
            ViewBag.TipoRecurso = _context.TipoRecursos.ToList();
            return View();
        }

        // Método para mostrar los resultados de búsqueda (usando GET)
        [HttpGet]
        public ActionResult Resultados(string autor, string palabraClave, int? idEdit, int? idTipoR)
        {
            var query = _context.Recursos
                .Include(r => r.AutoresRecursos)
                    .ThenInclude(ar => ar.Autor)
                .Include(r => r.Editorial)
                .Include(r => r.TipoRecurso)
                .AsQueryable();

            // Aplicar filtros según los parámetros recibidos
            if (!string.IsNullOrEmpty(autor))
            {
                query = query.Where(r => r.AutoresRecursos.Any(ar =>
                    ar.Autor.Nombres.Contains(autor) || ar.Autor.Apellidos.Contains(autor)));
            }

            if (!string.IsNullOrEmpty(palabraClave))
            {
                query = query.Where(r => r.PalabrasBusqueda != null &&
                    r.PalabrasBusqueda.Contains(palabraClave));
            }

            if (idEdit.HasValue && idEdit.Value > 0)
            {
                query = query.Where(r => r.IdEdit == idEdit.Value);
            }

            if (idTipoR.HasValue && idTipoR.Value > 0)
            {
                query = query.Where(r => r.IdTipoR == idTipoR.Value);
            }

            var resultados = query.ToList();
            return View(resultados);
        }

        // Método para listar por tipo y antigüedad
        [HttpGet]
        public ActionResult ListarPorTipo(int? idTipoR, int? aniosAntiguedad)
        {
            var query = _context.Recursos
                .Include(r => r.AutoresRecursos)
                    .ThenInclude(ar => ar.Autor)
                .Include(r => r.Editorial)
                .Include(r => r.TipoRecurso)
                .AsQueryable();

            // Filtrar por tipo si se especificó
            if (idTipoR.HasValue && idTipoR.Value > 0)
            {
                query = query.Where(r => r.IdTipoR == idTipoR.Value);
            }

            // Filtrar por antigüedad si se especificó
            if (aniosAntiguedad.HasValue)
            {
                int anioLimite = DateTime.Now.Year - aniosAntiguedad.Value;
                query = query.Where(r => r.annopublic.HasValue && r.annopublic >= anioLimite);
            }

            // Pasar datos al ViewBag para mantener los filtros seleccionados
            ViewBag.TiposRecurso = _context.TipoRecursos.ToList();
            ViewBag.TipoSeleccionado = idTipoR;
            ViewBag.AniosAntiguedad = aniosAntiguedad;

            var materiales = query.ToList();
            return View(materiales);
        }

        // Método para ver detalles de un recurso
        public ActionResult Details(int id)
        {
            var recurso = _context.Recursos
                .Include(r => r.AutoresRecursos)
                    .ThenInclude(ar => ar.Autor)
                .Include(r => r.Editorial)
                .Include(r => r.TipoRecurso)
                .Include(r => r.Pais)
                .FirstOrDefault(r => r.IdRec == id);

            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }
    }
}