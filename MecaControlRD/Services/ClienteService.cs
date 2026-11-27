using Aplicada1.Core;
using MecaControlRD.Data;
using MecaControlRD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MecaControlRD.Services;

public class ClienteService(
    IDbContextFactory<MecaControlDbContext> contextFactory
) : IServiceResult<Cliente, int>
{
    public async Task<Result> Guardar(Cliente cliente)
    {
        if (await Existe(cliente.ClienteId))
        {
            var modifico = await Modificar(cliente);

            return modifico
                ? Result.Success(true)
                : Result.Failure("No se pudo modificar el cliente.");
        }

        var inserto = await Insertar(cliente);

        return inserto
            ? Result.Success(true)
            : Result.Failure("No se pudo insertar el cliente.");
    }

    private async Task<bool> Existe(int clienteId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Clientes
            .AnyAsync(c => c.ClienteId == clienteId);
    }

    private async Task<bool> Insertar(Cliente cliente)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Clientes.Add(cliente);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Cliente cliente)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Clientes.Update(cliente);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Cliente?>> Buscar(int clienteId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Clientes
            .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
    }

    public async Task<Result<List<Cliente>>> GetList(
        Expression<Func<Cliente, bool>> criterio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Clientes
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int clienteId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        var eliminado = await contexto.Clientes
            .Where(c => c.ClienteId == clienteId)
            .ExecuteDeleteAsync() > 0;

        return eliminado
            ? Result.Success(true)
            : Result.Failure("No se encontró el cliente.");
    }
}