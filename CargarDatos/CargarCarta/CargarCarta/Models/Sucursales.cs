using System;
using System.Collections.Generic;

namespace CargarCarta.Models;

public partial class Sucursales
{
    public int IdSucursal { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Articulo> Articulo { get; set; } = new List<Articulo>();

    public virtual ICollection<ArticulosPorSucursal> ArticulosPorSucursals { get; set; } = new List<ArticulosPorSucursal>();
}
