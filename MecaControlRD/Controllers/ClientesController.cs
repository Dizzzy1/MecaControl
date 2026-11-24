using Microsoft.AspNetCore.Mvc;
using MecaControlRD.Models;
using MecaControlRD.Services;

namespace MecaControlRD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController(ClienteService clienteService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var resultado = await clienteService.GetList(c => true);
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resultado = await clienteService.Buscar(id);
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Cliente cliente)
    {
        var resultado = await clienteService.Guardar(cliente);
        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultado = await clienteService.Eliminar(id);
        return Ok(resultado);
    }
}