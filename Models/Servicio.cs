using System;
using System.Collections.Generic;

namespace MecaControlRD.Models;

public partial class Servicio
{
    public int ServiciosId { get; set; }

    public string? NombreServicio { get; set; }

    public decimal? PrecioBase { get; set; }

    public string? TiempoEstimado { get; set; }

    public virtual ICollection<Trabajo> Trabajos { get; set; } = new List<Trabajo>();
}
