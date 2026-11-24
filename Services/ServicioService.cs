using Aplicada1.Core;
using MecaControlRD.Data;
using MecaControlRD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MecaControlRD.Services;

public class ServicioService(
    IDbContextFactory<MecaControlDbContext> contextFactory
) : IServiceResult<Servicio, int>
{
    public async Task<Result> Guardar(Servicio servicio)
    {
        var existe = await Existe(servicio.ServiciosId);

        bool resultado;

        if (existe)
        {
            resultado = await Modificar(servicio);
        }
        else
        {
            resultado = await Insertar(servicio);
        }

        return Result.Success(resultado);
    }

    private async Task<bool> Existe(int serviciosId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Servicios
            .AnyAsync(s => s.ServiciosId == serviciosId);
    }

    private async Task<bool> Insertar(Servicio servicio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Servicios.Add(servicio);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Servicio servicio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Servicios.Update(servicio);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Servicio?>> Buscar(int serviciosId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Servicios
            .FirstOrDefaultAsync(s => s.ServiciosId == serviciosId);
    }

    public async Task<Result<List<Servicio>>> GetList(
        Expression<Func<Servicio, bool>> criterio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Servicios
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int serviciosId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        var eliminado = await contexto.Servicios
            .Where(s => s.ServiciosId == serviciosId)
            .ExecuteDeleteAsync() > 0;

        return Result.Success(eliminado);
    }
}