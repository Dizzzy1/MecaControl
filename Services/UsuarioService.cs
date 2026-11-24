using Aplicada1.Core;
using MecaControlRD.Data;
using MecaControlRD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MecaControlRD.Services;

public class UsuarioService(
    IDbContextFactory<MecaControlDbContext> contextFactory
) : IServiceResult<Usuario, int>
{
    public async Task<Result> Guardar(Usuario usuario)
    {
        var existe = await Existe(usuario.UsuarioId);

        bool resultado;

        if (existe)
        {
            resultado = await Modificar(usuario);
        }
        else
        {
            resultado = await Insertar(usuario);
        }

        return Result.Success(resultado);
    }

    private async Task<bool> Existe(int usuarioId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Usuarios
            .AnyAsync(u => u.UsuarioId == usuarioId);
    }

    private async Task<bool> Insertar(Usuario usuario)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Usuarios.Add(usuario);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Usuario usuario)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        contexto.Usuarios.Update(usuario);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Usuario?>> Buscar(int usuarioId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Usuarios
            .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);
    }

    public async Task<Result<List<Usuario>>> GetList(
        Expression<Func<Usuario, bool>> criterio)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        return await contexto.Usuarios
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int usuarioId)
    {
        await using var contexto =
            await contextFactory.CreateDbContextAsync();

        var eliminado = await contexto.Usuarios
            .Where(u => u.UsuarioId == usuarioId)
            .ExecuteDeleteAsync() > 0;

        return Result.Success(eliminado);
    }
}