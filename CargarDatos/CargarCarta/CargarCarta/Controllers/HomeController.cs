using CargarCarta.Models;
using CargarCarta.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CargarCarta.Controllers
{
    public class HomeController : Controller
    {
        private readonly Vvoucher2Context _VContext;

        public HomeController(Vvoucher2Context context)
        {
            _VContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cargar_Rubros()
        {
            return View();
        }

    }
}