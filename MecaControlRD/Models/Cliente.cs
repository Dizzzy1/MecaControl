using System;
using System.Collections.Generic;

namespace MecaControlRD.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public DateOnly? Fecharegistro { get; set; }

    public string? Apellido { get; set; }
}
