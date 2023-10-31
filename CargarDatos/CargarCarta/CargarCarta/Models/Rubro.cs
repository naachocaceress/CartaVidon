using System;
using System.Collections.Generic;

namespace CargarCarta.Models;

public partial class Rubro
{
    public int IdRubro { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Subrubro> Subrubros { get; set; } = new List<Subrubro>();
}
