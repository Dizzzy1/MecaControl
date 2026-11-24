using Microsoft.AspNetCore.Mvc;
using MecaControlRD.Models;
using MecaControlRD.Services;

namespace MecaControlRD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiculosController(VehiculoService vehiculoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var resultado = await vehiculoService.GetList(v => true);
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resultado = await vehiculoService.Buscar(id);
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Vehiculo vehiculo)
    {
        var resultado = await vehiculoService.Guardar(vehiculo);
        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultado = await vehiculoService.Eliminar(id);
        return Ok(resultado);
    }
}