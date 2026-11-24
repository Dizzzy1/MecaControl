using Aplicada1.Core;
using MecaControlRD.Data;
using MecaControlRD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MecaControlRD.Services;

public class VehiculoService(
    IDbContextFactory<MecaControlDbContext> contextFactory
) : IServiceResult<Vehiculo, int>
{
    public async Task<Result> Guardar(Vehiculo vehiculo)
    {
        var existe = await Existe(vehiculo.VehiculoId);

        bool resultado;

        if (existe)
        {
            resultado = await Modificar(vehiculo);
        }
        else
        {
            resultado = await Insertar(vehiculo);
        }

        return Result.Success(resultado);
    }

    private async Task<bool> Existe(int vehiculoId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Vehiculos
            .AnyAsync(v => v.VehiculoId == vehiculoId);
    }

    private async Task<bool> Insertar(Vehiculo vehiculo)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Vehiculos.Add(vehiculo);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Vehiculo vehiculo)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Vehiculos.Update(vehiculo);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Vehiculo?>> Buscar(int vehiculoId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Vehiculos
            .FirstOrDefaultAsync(v => v.VehiculoId == vehiculoId);
    }

    public async Task<Result<List<Vehiculo>>> GetList(
        Expression<Func<Vehiculo, bool>> criterio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Vehiculos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int vehiculoId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        var eliminado = await contexto.Vehiculos
            .Where(v => v.VehiculoId == vehiculoId)
            .ExecuteDeleteAsync() > 0;

        return Result.Success(eliminado);
    }
}