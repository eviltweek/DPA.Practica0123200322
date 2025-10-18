using System;
using System.Collections.Generic;
using DPA.Practica0123200322.CORE.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DPA.Practica0123200322.CORE.Infrastructure.Data;

public partial class UniversidadContext : DbContext
{
    public UniversidadContext()
    {
    }

    public UniversidadContext(DbContextOptions<UniversidadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrera> Carrera { get; set; }

    public virtual DbSet<Estudiante> Estudiante { get; set; }

    public virtual DbSet<Meta> Meta { get; set; }

    /*/
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=RODRIGO;Database=Universidad;User=sa;Pwd=123456789;TrustServerCertificate=True");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasIndex(e => e.Nombre, "UQ_Carrera_Nombre").IsUnique();

            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasIndex(e => new { e.Paterno, e.Materno }, "IX_Estudiante_Apellidos");

            entity.HasIndex(e => e.CarreraId, "IX_Estudiante_CarreraId");

            entity.HasIndex(e => e.Correo, "UQ_Estudiante_Correo").IsUnique();

            entity.Property(e => e.Correo).HasMaxLength(254);
            entity.Property(e => e.FechaRegistro)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Materno).HasMaxLength(100);
            entity.Property(e => e.Nombres).HasMaxLength(150);
            entity.Property(e => e.Paterno).HasMaxLength(100);

            entity.HasOne(d => d.Carrera).WithMany(p => p.Estudiante)
                .HasForeignKey(d => d.CarreraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estudiante_Carrera");
        });

        modelBuilder.Entity<Meta>(entity =>
        {
            entity.HasKey(e => e.Clave).HasName("PK____Meta__E8181E1085F75E71");

            entity.ToTable("__Meta");

            entity.Property(e => e.Clave).HasMaxLength(100);
            entity.Property(e => e.Fecha)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Valor).HasMaxLength(4000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
