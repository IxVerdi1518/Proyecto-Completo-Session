using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Servicios;
using WebApplication1.Sesion;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ServicioSingleton _servicioSingleton;
        private readonly ServicioSingleton _servicioSingleton2;
        private readonly ServicioTrasient _servicioTrasient;
        private readonly ServicioTrasient _servicioTrasient2;
        private readonly ServicioScope _servicioScope;
        private readonly ServicioScope _servicioScope2;
        public HomeController(ILogger<HomeController> logger, 
            ServicioSingleton servicioSingleton,
            ServicioSingleton servicioSingleton2,
            ServicioTrasient servicioTrasient, 
            ServicioTrasient servicioTrasient2, 
            ServicioScope servicioScope,
            ServicioScope servicioScope2)
        {
            _logger = logger;
            _servicioSingleton = servicioSingleton;
            _servicioSingleton2 = servicioSingleton2;
            _servicioTrasient = servicioTrasient;
            _servicioScope = servicioScope;
            _servicioTrasient2 = servicioTrasient2;
            _servicioScope2 = servicioScope2;
        }

        public IActionResult Index()
        {
            TempData["Singleton"] = "Sigleton: "+ _servicioSingleton.ObtenerGuid();
            TempData["Singleton2"] = "Sigleton2: "+ _servicioSingleton.ObtenerGuid();
            TempData["Trasient"] = "Trasient: " + _servicioTrasient.ObtenerGuid();
            TempData["Trasient2"] = "Trasient2: " + _servicioTrasient2.ObtenerGuid();
            TempData["Scope"] = "Scope: " + _servicioScope.ObtenerGuid();
            TempData["Scope2"] = "Scope2: " + _servicioScope2.ObtenerGuid();
            return View();
        }

        public IActionResult Empleado()
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