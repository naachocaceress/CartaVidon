using System;
using System.Collections.Generic;

namespace CargarCarta.Models;

public partial class Articulo
{
    public int IdArticulo { get; set; }

    public int? IdSubrubro { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public string? Descripcion { get; set; }

    public string? Receta { get; set; }

    public byte[]? Imagen { get; set; }

    public byte[]? Video { get; set; }

    public int? Puntos { get; set; }

    public virtual ICollection<ArticulosEtiqueta> ArticulosEtiqueta { get; set; } = new List<ArticulosEtiqueta>();

    public virtual ICollection<Sucursales> Sucursales { get; set; } = new List<Sucursales>();

    public virtual ICollection<ArticulosPorSucursal> ArticulosPorSucursals { get; set; } = new List<ArticulosPorSucursal>();

    public virtual Subrubro? oSubrubro { get; set; }

}
