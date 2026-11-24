using Aplicada1.Core;
using MecaControlRD.Data;
using MecaControlRD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MecaControlRD.Services;

public class PagoService(
    IDbContextFactory<MecaControlDbContext> contextFactory
) : IServiceResult<Pago, int>
{
    public async Task<Result> Guardar(Pago pago)
    {
        var existe = await Existe(pago.PagoId);

        bool resultado;

        if (existe)
        {
            resultado = await Modificar(pago);
        }
        else
        {
            resultado = await Insertar(pago);
        }

        return Result.Success(resultado);
    }

    private async Task<bool> Existe(int pagoId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Pagos
            .AnyAsync(p => p.PagoId == pagoId);
    }

    private async Task<bool> Insertar(Pago pago)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Pagos.Add(pago);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Pago pago)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Pagos.Update(pago);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Pago?>> Buscar(int pagoId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Pagos
            .FirstOrDefaultAsync(p => p.PagoId == pagoId);
    }

    public async Task<Result<List<Pago>>> GetList(
        Expression<Func<Pago, bool>> criterio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Pagos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int pagoId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        var eliminado = await contexto.Pagos
            .Where(p => p.PagoId == pagoId)
            .ExecuteDeleteAsync() > 0;

        return Result.Success(eliminado);
    }
}