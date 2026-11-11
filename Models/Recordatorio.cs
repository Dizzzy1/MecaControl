using System;
using System.Collections.Generic;

namespace MecaControlRD.Models;

public partial class Recordatorio
{
    public int RecordatorioId { get; set; }

    public int? TrabajoId { get; set; }

    public DateOnly? FechaEnvio { get; set; }
}
