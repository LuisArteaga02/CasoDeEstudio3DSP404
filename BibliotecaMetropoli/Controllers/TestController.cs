// Controllers/TestController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaMetropoli.Data;
using BibliotecaMetropoli.Data;

//integrantes
//Luis Enrique Cartagena Arteaga
//Elisabet Beatriz Marroquín González
//Verónica Elizabeth Rodríguez Majano
//Ever Gabriel Cabezas Alfaro
//Ricardo Daniel Alfaro Tomasino
namespace BibliotecaDB.Controllers
{
    public class TestController : Controller
    {
        private readonly BibliotecaMetropoliContext _context;

        public TestController(BibliotecaMetropoliContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Probar si la base de datos existe y puede conectarse
                var canConnect = await _context.Database.CanConnectAsync();

                if (canConnect)
                {
                    // Probar consulta simple
                    var autoresCount = await _context.Autores.CountAsync();
                    ViewBag.Message = $"La conexion se pudo establecer exitosamente {autoresCount}";
                }
                else
                {
                    ViewBag.Message = " No se pudo conectar a la base de datos";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    ViewBag.InnerException = $"Detalles: {ex.InnerException.Message}";
                }
            }

            return View();
        }
    }
}