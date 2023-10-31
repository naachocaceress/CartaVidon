using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CargarCarta.Models;

public partial class Vvoucher2Context : DbContext
{
    public Vvoucher2Context()
    {
    }

    public Vvoucher2Context(DbContextOptions<Vvoucher2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<ArticulosEtiqueta> ArticulosEtiquetas { get; set; }

    public virtual DbSet<ArticulosPorSucursal> ArticulosPorSucursals { get; set; }

    public virtual DbSet<Etiqueta> Etiquetas { get; set; }

    public virtual DbSet<Rubro> Rubros { get; set; }

    public virtual DbSet<Subrubro> Subrubros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("naachocaceres");

        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.IdArticulo).HasName("PK__Articulo__AABB74223F0496F4");

            entity.ToTable("Articulos", "Carta");

            entity.Property(e => e.IdArticulo).HasColumnName("idArticulo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdSubrubro).HasColumnName("idSubrubro");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Puntos).HasColumnName("puntos");
            entity.Property(e => e.Receta)
                .HasColumnType("text")
                .HasColumnName("receta");
            entity.Property(e => e.Video)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("video");

            entity.HasOne(d => d.oSubrubro).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdSubrubro)
                .HasConstraintName("FK__Articulos__idSub__01D345B0");
        });

        modelBuilder.Entity<ArticulosEtiqueta>(entity =>
        {
            entity.HasKey(e => e.IdArticuloEtiqueta).HasName("PK__Articulo__86AE9054EF1783A7");

            entity.ToTable("ArticulosEtiquetas", "Carta");

            entity.Property(e => e.IdArticuloEtiqueta).HasColumnName("idArticuloEtiqueta");
            entity.Property(e => e.IdArticulo).HasColumnName("idArticulo");
            entity.Property(e => e.IdEtiqueta).HasColumnName("idEtiqueta");

            entity.HasOne(d => d.oArticulo).WithMany(p => p.ArticulosEtiqueta)
                .HasForeignKey(d => d.IdArticulo)
                .HasConstraintName("FK__Articulos__idArt__0A688BB1");

            entity.HasOne(d => d.oEtiqueta).WithMany(p => p.ArticulosEtiqueta)
                .HasForeignKey(d => d.IdEtiqueta)
                .HasConstraintName("FK__Articulos__idEti__0B5CAFEA");
        });

        modelBuilder.Entity<ArticulosPorSucursal>(entity =>
        {
            entity.HasKey(e => e.IdArticuloPorSucursal).HasName("PK__Articulo__B09791D6C9008C1E");

            entity.ToTable("ArticulosPorSucursal", "Carta");

            entity.Property(e => e.IdArticuloPorSucursal).HasColumnName("idArticuloPorSucursal");
            entity.Property(e => e.IdArticulo).HasColumnName("idArticulo");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.oArticulo).WithMany(p => p.ArticulosPorSucursals)
                .HasForeignKey(d => d.IdArticulo)
                .HasConstraintName("FK__Articulos__idArt__04AFB25B");
        });

        modelBuilder.Entity<Etiqueta>(entity =>
        {
            entity.HasKey(e => e.IdEtiqueta).HasName("PK__Etiqueta__3C1526A7CDAA1FAD");

            entity.ToTable("Etiquetas", "Carta");

            entity.Property(e => e.IdEtiqueta).HasColumnName("idEtiqueta");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Rubro>(entity =>
        {
            entity.HasKey(e => e.IdRubro).HasName("PK__Rubros__A32E88745872F6F5");

            entity.ToTable("Rubros", "Carta");

            entity.Property(e => e.IdRubro).HasColumnName("idRubro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Subrubro>(entity =>
        {
            entity.HasKey(e => e.IdSubrubro).HasName("PK__Subrubro__9D45814F511DB014");

            entity.ToTable("Subrubros", "Carta");

            entity.Property(e => e.IdSubrubro).HasColumnName("idSubrubro");
            entity.Property(e => e.IdRubro).HasColumnName("idRubro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.oRubro).WithMany(p => p.Subrubros)
                .HasForeignKey(d => d.IdRubro)
                .HasConstraintName("FK__Subrubros__idRub__7EF6D905");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
