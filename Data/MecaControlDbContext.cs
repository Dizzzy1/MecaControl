using System;
using System.Collections.Generic;
using MecaControlRD.Models;
using Microsoft.EntityFrameworkCore;

namespace MecaControlRD.Data;

public partial class MecaControlDbContext : DbContext
{
    public MecaControlDbContext(DbContextOptions<MecaControlDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Recordatorio> Recordatorios { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Trabajo> Trabajos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A73EDA885B");

            entity.Property(e => e.ClienteId)
                .ValueGeneratedNever()
                .HasColumnName("ClienteID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(110)
                .IsUnicode(false);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pagos__F00B61583BD9ED00");

            entity.Property(e => e.PagoId)
                .ValueGeneratedNever()
                .HasColumnName("PagoID");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TrabajoId).HasColumnName("TrabajoID");
        });

        modelBuilder.Entity<Recordatorio>(entity =>
        {
            entity.HasKey(e => e.RecordatorioId).HasName("PK__Recordat__9E994608E71575E7");

            entity.Property(e => e.RecordatorioId)
                .ValueGeneratedNever()
                .HasColumnName("RecordatorioID");
            entity.Property(e => e.TrabajoId).HasColumnName("TrabajoID");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServiciosId).HasName("PK__Servicio__ED7749BA64835AD0");

            entity.Property(e => e.ServiciosId)
                .ValueGeneratedNever()
                .HasColumnName("ServiciosID");
            entity.Property(e => e.NombreServicio).HasMaxLength(100);
            entity.Property(e => e.PrecioBase).HasColumnType("money");
            entity.Property(e => e.TiempoEstimado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Trabajo>(entity =>
        {
            entity.HasKey(e => e.TrabajoId).HasName("PK__Trabajos__FCD3CD5E1EE0121C");

            entity.Property(e => e.TrabajoId)
                .ValueGeneratedNever()
                .HasColumnName("TrabajoID");
            entity.Property(e => e.CostoManoObra).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CostoRepuestos).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.ServiciosId).HasColumnName("ServiciosID");
            entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");

            entity.HasOne(d => d.Servicios).WithMany(p => p.Trabajos)
                .HasForeignKey(d => d.ServiciosId)
                .HasConstraintName("FK_Trabajo_Servicios");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.Trabajos)
                .HasForeignKey(d => d.VehiculoId)
                .HasConstraintName("FK_Trabajo_Vehiculo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7983CD6E922");

            entity.ToTable("Usuario");

            entity.Property(e => e.UsuarioId)
                .ValueGeneratedNever()
                .HasColumnName("UsuarioID");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(110)
                .IsUnicode(false);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.VehiculoId).HasName("PK__Vehiculo__AA0886206523DC15");

            entity.Property(e => e.VehiculoId)
                .ValueGeneratedNever()
                .HasColumnName("VehiculoID");
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Marca).HasMaxLength(50);
            entity.Property(e => e.Modelo).HasMaxLength(50);
            entity.Property(e => e.Placa).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
