using System;
using System.Collections.Generic;

namespace CargarCarta.Models;

public partial class Etiqueta
{
    public int IdEtiqueta { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<ArticulosEtiqueta> ArticulosEtiqueta { get; set; } = new List<ArticulosEtiqueta>();
}
