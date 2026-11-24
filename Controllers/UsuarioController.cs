using Microsoft.AspNetCore.Mvc;
using MecaControlRD.Models;
using MecaControlRD.Services;
using MecaControlRD.DTOs;

namespace MecaControlRD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController(UsuarioService usuarioService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var resultado = await usuarioService.GetList(u => true);

        var usuarios = resultado.Value?
            .Select(u => new UsuarioDTO(
                u.UsuarioId,
                u.Nombre,
                u.Correo,
                u.Rol
            ));

        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resultado = await usuarioService.Buscar(id);

        if (resultado.Value == null)
            return NotFound("Usuario no encontrado.");

        var usuario = resultado.Value;

        var usuarioDTO = new UsuarioDTO(
            usuario.UsuarioId,
            usuario.Nombre,
            usuario.Correo,
            usuario.Rol
        );

        return Ok(usuarioDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Usuario usuario)
    {
        var resultado = await usuarioService.Guardar(usuario);
        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultado = await usuarioService.Eliminar(id);
        return Ok(resultado);
    }
}