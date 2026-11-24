using System;
using System.Collections.Generic;

namespace MecaControlRD.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int? TrabajoId { get; set; }

    public DateOnly? FechaPago { get; set; }

    public decimal? Monto { get; set; }
}
