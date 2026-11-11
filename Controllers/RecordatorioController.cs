using Microsoft.AspNetCore.Mvc;
using MecaControlRD.Models;
using MecaControlRD.Services;

namespace MecaControlRD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordatoriosController(
    RecordatorioService recordatorioService
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var resultado = await recordatorioService.GetList(r => true);
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resultado = await recordatorioService.Buscar(id);
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Recordatorio recordatorio)
    {
        var resultado = await recordatorioService.Guardar(recordatorio);
        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultado = await recordatorioService.Eliminar(id);
        return Ok(resultado);
    }
}