using System;
using System.Collections.Generic;

namespace MecaControlRD.Models;

public partial class Trabajo
{
    public int TrabajoId { get; set; }

    public int? VehiculoId { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public DateOnly? FechaSalida { get; set; }

    public string? Descripcion { get; set; }

    public decimal? CostoManoObra { get; set; }

    public decimal? CostoRepuestos { get; set; }

    public int? Total { get; set; }

    public string? Estado { get; set; }

    public int? ServiciosId { get; set; }

    public virtual Servicio? Servicios { get; set; }

    public virtual Vehiculo? Vehiculo { get; set; }
}
