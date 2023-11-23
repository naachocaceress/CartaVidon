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

        public async Task<IActionResult> Cargar<T>(T obj, Func<T, Task<T>> findExisting)
        {
            var existing = await findExisting(obj);

            if (existing != null)
            {
                return Json(new { error = $"Ya existe un {typeof(T).Name} con ese nombre." });
            }
            else
            {
                _VContext.Add(obj);
                await _VContext.SaveChangesAsync();
                return Json(obj);
            }
        }

        public enum EntityType
        {
            Rubro,
            Subrubro,
            Etiqueta
        }

        public async Task<IActionResult> Borrar(int? id, EntityType entityType)
        {
            if (id == null)
            {
                return NotFound();
            }

            switch (entityType)
            {
                case EntityType.Rubro:
                    var rubro = _VContext.Rubros.Find(id);
                    if (rubro == null)
                    {
                        return NotFound();
                    }

                    // Encuentra los Subrubros asociados con este Rubro
                    var subrubros = _VContext.Subrubros.Where(s => s.IdRubro == id);

                    // Establece la clave foránea a null
                    foreach (var subrubroe in subrubros)
                    {
                        subrubroe.IdRubro = null;
                    }

                    // Guarda los cambios en los Subrubros
                    await _VContext.SaveChangesAsync();

                    // Ahora puedes borrar el Rubro
                    _VContext.Rubros.Remove(rubro);
                    break;

                case EntityType.Subrubro:
                    var subrubro = _VContext.Subrubros.Find(id);
                    if (subrubro == null)
                    {
                        return NotFound();
                    }

                    // Ahora puedes borrar el Subrubro
                    _VContext.Subrubros.Remove(subrubro);
                    break;

                case EntityType.Etiqueta:
                    var etiqueta = _VContext.Etiquetas.Find(id);
                    if (etiqueta == null)
                    {
                        return NotFound();
                    }

                    // Ahora puedes borrar la Etiqueta
                    _VContext.Etiquetas.Remove(etiqueta);
                    break;

                default:
                    return NotFound();
            }

            await _VContext.SaveChangesAsync();

            return RedirectToAction(nameof(Rubros));
        }


        //--

        [HttpPost]
        public async Task<IActionResult> CargarRubro(Rubro rubro)
        {
            return await Cargar(rubro, async r => await _VContext.Rubros.FirstOrDefaultAsync(x => x.Nombre == r.Nombre));
        }      

        //

        [HttpPost]
        public async Task<IActionResult> CargarSubrubro(Subrubro subrubro)
        {
            var result = await Cargar(subrubro, async r => await _VContext.Subrubros.FirstOrDefaultAsync(x => x.Nombre == r.Nombre && x.IdRubro == r.IdRubro));

            if (result is JsonResult jsonResult && jsonResult.Value is Subrubro newSubrubro)
            {
                var rubro = await _VContext.Rubros.FirstOrDefaultAsync(r => r.IdRubro == newSubrubro.IdRubro);
                var rubroNombre = rubro != null ? rubro.Nombre : "";
                return Json(new { newSubrubro.IdSubrubro, newSubrubro.Nombre, RubroNombre = rubroNombre, IdRubro = rubro.IdRubro });
            }

            return result;
        }

        //

        [HttpPost]
        public async Task<IActionResult> CargarEtiqueta(Etiqueta etiqueta)
        {
            return await Cargar(etiqueta, async e => await _VContext.Etiquetas.FirstOrDefaultAsync(x => x.Nombre == e.Nombre));
        }

        //
        

        [HttpGet]
        public async Task<IActionResult> ObtenerRubros()
        {
            var rubros = await _VContext.Rubros.ToListAsync();
            return Json(rubros.Select(r => new { valor = r.IdRubro, texto = r.Nombre }));
        }


        [HttpPost]
        public async Task<IActionResult> EditarSubrubro(int? IdSubrubro, string Nombre)
        {
            if (IdSubrubro == null)
            {
                return NotFound();
            }

            var subrubro = _VContext.Subrubros.Find(IdSubrubro);
            if (subrubro == null)
            {
                return NotFound();
            }

            subrubro.Nombre = Nombre;  // Actualiza el nombre del rubro con el nuevo nombre
            _VContext.Subrubros.Update(subrubro);
            _VContext.SaveChanges();

            return RedirectToAction(nameof(Rubros));
        }


        [HttpPost]
        public async Task<IActionResult> EditarRubro(int? IdRubro, string Nombre)
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

        [HttpPost]
        public async Task<IActionResult> EditarEtiqueta(int? IdEtiqueta, string Nombre)
        {
            if (IdEtiqueta == null)
            {
                return NotFound();
            }

            var etiqueta = _VContext.Etiquetas.Find(IdEtiqueta);
            if (etiqueta == null)
            {
                return NotFound();
            }

            etiqueta.Nombre = Nombre;  // Actualiza el nombre del rubro con el nuevo nombre
            _VContext.Etiquetas.Update(etiqueta);
            _VContext.SaveChanges();

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