using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Registro_de_mascotas.Data;
using Registro_de_mascotas.Models;

namespace Registro_de_mascotas.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var mascotas = _context.DataMascota.OrderBy(m => m.Id).ToList();
        return View(mascotas);
    }

    public IActionResult Privacy()
    {
        return View();
    }


    public IActionResult Delete(long id)
    {
        var mascota = _context.DataMascota.Find(id);
        if (mascota != null)
        {
            _context.DataMascota.Remove(mascota);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Editar(long id)
    {
        var mascota = _context.DataMascota.Find(id);
        if (mascota == null)
        {
            return NotFound();
        }
        ViewData["Controlador"] = "Home";
        ViewData["Action"] = "Actualizar";
        return View("~/Views/Registro/Index.cshtml", mascota);
    }

    [HttpPost]
    public IActionResult Actualizar(Mascota m)
    {
        if (m.FechaNacimiento.Kind == DateTimeKind.Unspecified)
        {
            m.FechaNacimiento = DateTime.SpecifyKind(m.FechaNacimiento, DateTimeKind.Utc);
        }
        _context.DataMascota.Update(m);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
