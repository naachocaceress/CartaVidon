using System;
using System.Collections.Generic;

namespace CargarCarta.Models;

public partial class ArticulosEtiqueta
{
    public int IdArticuloEtiqueta { get; set; }

    public int? IdArticulo { get; set; }

    public int? IdEtiqueta { get; set; }

    public virtual Articulo? oArticulo { get; set; }

    public virtual Etiqueta? oEtiqueta { get; set; }
}
