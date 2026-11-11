using System;
using System.Collections.Generic;

namespace MecaControlRD.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Contrasenia { get; set; }

    public string? Rol { get; set; }
}
