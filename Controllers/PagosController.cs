using Microsoft.AspNetCore.Mvc;
using MecaControlRD.Models;
using MecaControlRD.Services;

namespace MecaControlRD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagosController(PagoService pagoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var resultado = await pagoService.GetList(p => true);
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resultado = await pagoService.Buscar(id);
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Pago pago)
    {
        var resultado = await pagoService.Guardar(pago);
        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultado = await pagoService.Eliminar(id);
        return Ok(resultado);
    }
}