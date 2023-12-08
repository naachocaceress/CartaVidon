using System;
using System.Collections.Generic;

namespace CargarCarta.Models;

public partial class ArticulosPorSucursal
{
    public int IdArticuloPorSucursal { get; set; }

    public int? IdArticulo { get; set; }

    public int? IdSucursal { get; set; }

    public decimal? Precio { get; set; }

    public virtual Articulo? oArticulo { get; set; }

    public virtual Sucursales? oSucursales { get; set; }

}
