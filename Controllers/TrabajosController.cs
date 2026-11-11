using Microsoft.AspNetCore.Mvc;
using MecaControlRD.Models;
using MecaControlRD.Services;

namespace MecaControlRD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrabajosController(TrabajoService trabajoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var resultado = await trabajoService.GetList(t => true);
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resultado = await trabajoService.Buscar(id);
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Trabajo trabajo)
    {
        var resultado = await trabajoService.Guardar(trabajo);
        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultado = await trabajoService.Eliminar(id);
        return Ok(resultado);
    }
}