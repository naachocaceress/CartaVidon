using CargarCarta.Models;
using CargarCarta.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CargarCarta.Controllers
{
    public class CartaController : Controller
    {
        private readonly Vvoucher2Context _VContext;

        public CartaController(Vvoucher2Context context)
        {
            _VContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //--

        public class GestionViewModel
        {
            public Rubro Rubro { get; set; }
            public IEnumerable<Rubro> RubroTabla { get; set; }

            public Subrubro Subrubro { get; set; }
            public IEnumerable<Subrubro> SubrubroTabla { get; set; }

            public Etiqueta Etiqueta { get; set; }
            public IEnumerable<Etiqueta> EtiquetaTabla { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> Rubros()
        {
            var rubros = await _VContext.Rubros.ToListAsync();
            var subrubros = await _VContext.Subrubros.ToListAsync();
            var etiquetas = await _VContext.Etiquetas.ToListAsync();
            var model = new GestionViewModel { RubroTabla = rubros, SubrubroTabla = subrubros, EtiquetaTabla = etiquetas };

            return View(model);
        }


        //

        [HttpPost]
        public async Task<IActionResult> CargarRubro (Rubro rubro)
        {
            _VContext.Rubros.Add(rubro);
            await _VContext.SaveChangesAsync();
            return RedirectToAction(nameof(Rubros));
        }


        [HttpPost]
        public async Task<IActionResult> BorrarRubro(int? IdRubro)
        {
            if (IdRubro == null)
            {
                return NotFound();
            }

            var rubro = _VContext.Rubros.Find(IdRubro);
            if (rubro == null)
            {
                return NotFound();
            }

            // Encuentra los Subrubros asociados con este Rubro
            var subrubros = _VContext.Subrubros.Where(s => s.IdRubro == IdRubro);

            // Establece la clave foránea a null
            foreach (var subrubro in subrubros)
            {
                subrubro.IdRubro = null;
            }

            // Guarda los cambios en los Subrubros
            await _VContext.SaveChangesAsync();

            // Ahora puedes borrar el Rubro
            _VContext.Rubros.Remove(rubro);
            await _VContext.SaveChangesAsync();

            return RedirectToAction(nameof(Rubros));
        }

        [HttpPost]
        public async Task<IActionResult> EditarRubro(int? IdRubro, string Nombre)  // Recibe el nuevo nombre como parámetro
        {
            if (IdRubro == null)
            {
                return NotFound();
            }

            var rubro = _VContext.Rubros.Find(IdRubro);
            if (rubro == null)
            {
                return NotFound();
            }

            rubro.Nombre = Nombre;  // Actualiza el nombre del rubro con el nuevo nombre
            _VContext.Rubros.Update(rubro);
            _VContext.SaveChanges();

            return RedirectToAction(nameof(Rubros));
        }



        //

        [HttpPost]
        public async Task<IActionResult> CargarSubrubro (Subrubro subrubro)
        {
            _VContext.Subrubros.Add(subrubro);
            await _VContext.SaveChangesAsync();
            return RedirectToAction(nameof(Rubros));
        }

        //

        [HttpPost]
        public async Task<IActionResult> CargarEtiqueta(Etiqueta etiqueta)
        {
            _VContext.Etiquetas.Add(etiqueta);
            await _VContext.SaveChangesAsync();
            return RedirectToAction(nameof(Rubros));
        }

        //--

        [HttpGet]
        public IActionResult Articulos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Precios()
        {
            return View();
        }

    }
}