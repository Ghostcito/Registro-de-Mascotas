using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Registro_de_mascotas.Data;
using Registro_de_mascotas.Models;

namespace Registro_de_mascotas.Controllers
{
    [Route("[controller]")]
    public class RegistroController : Controller
    {
        private readonly ILogger<RegistroController> _logger;
        private readonly ApplicationDbContext _context;

        public RegistroController(ILogger<RegistroController> logger,ApplicationDbContext context )
        {
            _logger = logger;
            _context= context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Controlador"]="Registro";
            ViewData["Action"]="Registrar";
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Mascota m)
        {
            registrarObj(m);
            ViewData["Confirm"]="Mascota registrada";
            return RedirectToAction("Index","Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public void registrarObj(Mascota m){
            if (m.FechaNacimiento.Kind == DateTimeKind.Unspecified)
            {
                m.FechaNacimiento= DateTime.SpecifyKind(m.FechaNacimiento, DateTimeKind.Utc);
            }
            _context.Add(m);
            _context.SaveChanges();
        }
    }
}