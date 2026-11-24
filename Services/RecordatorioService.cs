using Aplicada1.Core;
using MecaControlRD.Data;
using MecaControlRD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MecaControlRD.Services;

public class RecordatorioService(
    IDbContextFactory<MecaControlDbContext> contextFactory
) : IServiceResult<Recordatorio, int>
{
    public async Task<Result> Guardar(Recordatorio recordatorio)
    {
        var existe = await Existe(recordatorio.RecordatorioId);

        bool resultado;

        if (existe)
        {
            resultado = await Modificar(recordatorio);
        }
        else
        {
            resultado = await Insertar(recordatorio);
        }

        return Result.Success(resultado);
    }

    private async Task<bool> Existe(int recordatorioId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Recordatorios
            .AnyAsync(r => r.RecordatorioId == recordatorioId);
    }

    private async Task<bool> Insertar(Recordatorio recordatorio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Recordatorios.Add(recordatorio);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Recordatorio recordatorio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Recordatorios.Update(recordatorio);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Recordatorio?>> Buscar(int recordatorioId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Recordatorios
            .FirstOrDefaultAsync(r => r.RecordatorioId == recordatorioId);
    }

    public async Task<Result<List<Recordatorio>>> GetList(
        Expression<Func<Recordatorio, bool>> criterio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Recordatorios
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int recordatorioId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        var eliminado = await contexto.Recordatorios
            .Where(r => r.RecordatorioId == recordatorioId)
            .ExecuteDeleteAsync() > 0;

        return Result.Success(eliminado);
    }
}