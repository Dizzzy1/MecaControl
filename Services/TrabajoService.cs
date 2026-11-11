using Aplicada1.Core;
using MecaControlRD.Data;
using MecaControlRD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MecaControlRD.Services;

public class TrabajoService(
    IDbContextFactory<MecaControlDbContext> contextFactory
) : IServiceResult<Trabajo, int>
{
    public async Task<Result> Guardar(Trabajo trabajo)
    {
        var existe = await Existe(trabajo.TrabajoId);

        bool resultado;

        if (existe)
        {
            resultado = await Modificar(trabajo);
        }
        else
        {
            resultado = await Insertar(trabajo);
        }

        return Result.Success(resultado);
    }

    private async Task<bool> Existe(int trabajoId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Trabajos
            .AnyAsync(t => t.TrabajoId == trabajoId);
    }

    private async Task<bool> Insertar(Trabajo trabajo)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Trabajos.Add(trabajo);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Trabajo trabajo)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Trabajos.Update(trabajo);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Trabajo?>> Buscar(int trabajoId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Trabajos
            .Include(t => t.Vehiculo)
            .Include(t => t.Servicios)
            .FirstOrDefaultAsync(t => t.TrabajoId == trabajoId);
    }

    public async Task<Result<List<Trabajo>>> GetList(
        Expression<Func<Trabajo, bool>> criterio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Trabajos
            .Include(t => t.Vehiculo)
            .Include(t => t.Servicios)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int trabajoId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        var eliminado = await contexto.Trabajos
            .Where(t => t.TrabajoId == trabajoId)
            .ExecuteDeleteAsync() > 0;

        return Result.Success(eliminado);
    }
}