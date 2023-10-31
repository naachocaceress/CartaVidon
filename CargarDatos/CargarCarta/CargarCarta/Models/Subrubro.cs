using System;
using System.Collections.Generic;

namespace CargarCarta.Models;

public partial class Subrubro
{
    public int IdSubrubro { get; set; }

    public int? IdRubro { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

    public virtual Rubro? oRubro { get; set; }
}
