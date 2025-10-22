using System.Diagnostics;
using BibliotecaMetropoli.Models;
using Microsoft.AspNetCore.Mvc;
//integrantes
//Luis Enrique Cartagena Arteaga
//Elisabet Beatriz Marroquín González
//Verónica Elizabeth Rodríguez Majano
//Ever Gabriel Cabezas Alfaro
//Ricardo Daniel Alfaro Tomasino

namespace BibliotecaMetropoli.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
