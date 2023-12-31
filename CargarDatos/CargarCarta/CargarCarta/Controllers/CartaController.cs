﻿using CargarCarta.Models;
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


        #region Rubros

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

        // --


        [HttpPost]
        public async Task<IActionResult> CargarRubro(Rubro rubro)
        {
            return await Cargar(rubro, async r => await _VContext.Rubros.FirstOrDefaultAsync(x => x.Nombre == r.Nombre));
        }

        [HttpPost]
        public async Task<IActionResult> CargarSubrubro(Subrubro subrubro)
        {
            var result = await Cargar(subrubro, async r => await _VContext.Subrubros.FirstOrDefaultAsync(x => x.Nombre == r.Nombre && x.IdRubro == r.IdRubro));

            if (result is JsonResult jsonResult && jsonResult.Value is Subrubro newSubrubro)
            {
                var rubro = await _VContext.Rubros.FirstOrDefaultAsync(r => r.IdRubro == newSubrubro.IdRubro);
                var rubroNombre = rubro != null ? rubro.Nombre : "";
                return Json(new { newSubrubro.IdSubrubro, newSubrubro.Nombre, RubroNombre = rubroNombre, idRubro = rubro.IdRubro });
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CargarEtiqueta(Etiqueta etiqueta)
        {
            return await Cargar(etiqueta, async e => await _VContext.Etiquetas.FirstOrDefaultAsync(x => x.Nombre == e.Nombre));
        }

        // --

        public enum EntityType
        {
            Rubro,
            Subrubro,
            Etiqueta
        }

        [HttpPost]
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

        // --


        [HttpGet]
        public async Task<IActionResult> GetSubrubros()
        {
            var subrubros = await _VContext.Subrubros.Include(s => s.oRubro).ToListAsync();
            var data = new List<object>();
            foreach (var subrubro in subrubros)
            {
                data.Add(new
                {
                    idSubrubro = subrubro.IdSubrubro,
                    nombre = subrubro.Nombre,
                    idRubro = subrubro.oRubro?.IdRubro,  // Agrega el ID del rubro aquí
                    rubroNombre = subrubro.oRubro?.Nombre
                });
            }
            return Json(data);
        }       

        [HttpGet]
        public async Task<IActionResult> ObtenerRubros()
        {
            var rubros = await _VContext.Rubros.ToListAsync();
            return Json(rubros.Select(r => new { valor = r.IdRubro, texto = r.Nombre }));
        }


        [HttpPost]
        public async Task<IActionResult> EditarSubrubro(int? IdSubrubro, string Nombre, int? IdRubro)
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

            var existeSubrubro = _VContext.Subrubros.Any(s => s.Nombre == Nombre && s.IdRubro == IdRubro);
            if (existeSubrubro)
            {
                return Json(new { success = false, message = "Ya existe un subrubro con ese nombre y rubro." });
            }

            subrubro.Nombre = Nombre; 
            subrubro.IdRubro = IdRubro;
            _VContext.Subrubros.Update(subrubro);
            _VContext.SaveChanges();

            var rubro = _VContext.Rubros.Find(IdRubro);
            var rubroNombre = rubro != null ? rubro.Nombre : null;

            return Json(new { success = true, rubroNombre = rubroNombre });
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

            var existeRubro = _VContext.Rubros.Any(s => s.Nombre == Nombre);
            if (existeRubro)
            {
                return Json(new { success = false, message = "Ya existe un rubro con ese nombre." });
            }

            rubro.Nombre = Nombre; 
            _VContext.Rubros.Update(rubro);
            _VContext.SaveChanges();

            return Json(new { success = true});
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

            var existeEtiqueta = _VContext.Etiquetas.Any(s => s.Nombre == Nombre);
            if (existeEtiqueta)
            {
                return Json(new { success = false, message = "Ya existe una etiqueta con ese nombre." });
            }

            etiqueta.Nombre = Nombre;
            _VContext.Etiquetas.Update(etiqueta);
            _VContext.SaveChanges();

            return Json(new { success = true });
        }

        #endregion

        //--

        #region Articulos

        public class GestionArticulosViewModel
        {
            public Articulo Articulo { get; set; }
            public IEnumerable<Articulo> ArticuloTabla { get; set; }

            public ArticulosPorSucursal ArtiSucu { get; set; }
            public IEnumerable<ArticulosPorSucursal> ArtiSucuTabla { get; set; }

            public ArticulosEtiqueta ArtiEti { get; set; }
            public IEnumerable<ArticulosEtiqueta> ArtiEtiTabla { get; set; }

            public Subrubro Subrubro { get; set; }
            public IEnumerable<Subrubro> SubrubroTabla { get; set; }

            public Etiqueta Etiqueta { get; set; }
            public IEnumerable<Etiqueta> EtiquetaTabla { get; set; }
            public List<int> IdEtiqueta { get; set; }

            public Sucursales Sucursal { get; set; }
            public IEnumerable<Sucursales> SucursalTabla { get; set; }
            public List<int> IdSucursal { get; set; }

            public Rubro Rubro { get; set; }
            public IEnumerable<Rubro> RubroTabla { get; set; }

            public IFormFile Imagen { get; set; }
            public IFormFile Video { get; set; }
        }


        [HttpGet]
        public async Task<IActionResult> Articulos()
        {
            var articulos = await _VContext.Articulos.ToListAsync();
            var artisucu = await _VContext.ArticulosPorSucursals.ToListAsync();
            var artieti = await _VContext.ArticulosEtiquetas.ToListAsync();
            var subrubros = await _VContext.Subrubros.ToListAsync();
            var etiquetas = await _VContext.Etiquetas.ToListAsync();
            var sucursales = await _VContext.Sucusales.ToListAsync();

            var model = new GestionArticulosViewModel { ArticuloTabla = articulos, ArtiSucuTabla = artisucu, ArtiEtiTabla = artieti, SubrubroTabla = subrubros, EtiquetaTabla = etiquetas, SucursalTabla = sucursales };

            return View(model);
        }

        //--

        [HttpPost]
        public async Task<IActionResult> CargarArticulo(GestionArticulosViewModel viewModel, Articulo articulos, IFormFile imagen, IFormFile video)
        {
            if (_VContext.Articulos.Any(a => a.Nombre == viewModel.Articulo.Nombre))
            {
                ModelState.AddModelError("", "El nombre del artículo ya existe.");
                return View(viewModel);
            }

            if (imagen != null && imagen.Length > 0)
            {
                using var ms = new MemoryStream();
                await imagen.CopyToAsync(ms);
                viewModel.Articulo.Imagen = ms.ToArray();
            }

            if (video != null && video.Length > 0)
            {
                using var ms = new MemoryStream();
                await video.CopyToAsync(ms);
                viewModel.Articulo.Video = ms.ToArray();
            }

            _VContext.Articulos.Add(viewModel.Articulo);
            await _VContext.SaveChangesAsync();
            articulos = viewModel.Articulo;

            if (viewModel.IdSucursal != null)
            {
                foreach (var idSucursal in viewModel.IdSucursal)
                {
                    var articulosPorSucursal = new ArticulosPorSucursal();
                    articulosPorSucursal.IdArticulo = articulos.IdArticulo;
                    articulosPorSucursal.IdSucursal = idSucursal;  // Esto es un entero, no una lista
                    _VContext.ArticulosPorSucursals.Add(articulosPorSucursal);
                }
            }
            await _VContext.SaveChangesAsync();

            if (viewModel.IdEtiqueta != null)
            {
                foreach (var idEtiqueta in viewModel.IdEtiqueta)
                {
                    var articulosEtiqueta = new ArticulosEtiqueta();
                    articulosEtiqueta.IdArticulo = articulos.IdArticulo;
                    articulosEtiqueta.IdEtiqueta = idEtiqueta;
                    _VContext.ArticulosEtiquetas.Add(articulosEtiqueta);
                }
            }
            await _VContext.SaveChangesAsync();

            return RedirectToAction(nameof(Articulos));
        }

        #endregion

        //--

        [HttpGet]
        public async Task<IActionResult> Precios()
        {
            var articulos = await _VContext.Articulos.ToListAsync();
            var subrubros = await _VContext.Subrubros.ToListAsync();
            var etiquetas = await _VContext.Etiquetas.ToListAsync();
            var sucursales = await _VContext.Sucusales.ToListAsync();
            var rubros = await _VContext.Rubros.ToListAsync();
            var model = new GestionArticulosViewModel { ArticuloTabla = articulos, SubrubroTabla = subrubros, EtiquetaTabla = etiquetas, SucursalTabla = sucursales, RubroTabla = rubros };

            return View(model);
        }

    }
}