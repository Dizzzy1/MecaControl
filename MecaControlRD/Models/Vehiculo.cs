using System;
using System.Collections.Generic;

namespace MecaControlRD.Models;

public partial class Vehiculo
{
    public int VehiculoId { get; set; }

    public int? ClienteId { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public int? Anio { get; set; }

    public string? Placa { get; set; }

    public string? Color { get; set; }

    public virtual ICollection<Trabajo> Trabajos { get; set; } = new List<Trabajo>();
}
