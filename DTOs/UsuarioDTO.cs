namespace MecaControlRD.DTOs;

public record UsuarioDTO(
    int UsuarioId,
    string? Nombre,
    string? Correo,
    string? Rol
);