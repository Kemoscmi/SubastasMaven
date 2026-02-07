using System;
using System.Collections.Generic;
using Maven.Infraestructure.MavenModels;
using Microsoft.EntityFrameworkCore;

namespace Maven.Infraestructure.MavenData;

public partial class MavenContext : DbContext
{
    public MavenContext(DbContextOptions<MavenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaJoya> CategoriaJoya { get; set; }

    public virtual DbSet<CondicionObjeto> CondicionObjeto { get; set; }

    public virtual DbSet<EstadoObjeto> EstadoObjeto { get; set; }

    public virtual DbSet<EstadoPago> EstadoPago { get; set; }

    public virtual DbSet<EstadoSubasta> EstadoSubasta { get; set; }

    public virtual DbSet<EstadoUsuario> EstadoUsuario { get; set; }

    public virtual DbSet<Joya> Joya { get; set; }

    public virtual DbSet<JoyaImagen> JoyaImagen { get; set; }

    public virtual DbSet<Notificacion> Notificacion { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Puja> Puja { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Subasta> Subasta { get; set; }

    public virtual DbSet<SubastaResultado> SubastaResultado { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaJoya>(entity =>
        {
            entity.HasKey(e => e.CategoriaJoyaId).HasName("PK__Categori__FA10E2A102F73899");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCFF1DFA1AA").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(60);
        });

        modelBuilder.Entity<CondicionObjeto>(entity =>
        {
            entity.HasKey(e => e.CondicionObjetoId).HasName("PK__Condicio__301A99D38764B17B");

            entity.HasIndex(e => e.NombreCondicion, "UQ__Condicio__71AAD23D343E2067").IsUnique();

            entity.Property(e => e.NombreCondicion).HasMaxLength(20);
        });

        modelBuilder.Entity<EstadoObjeto>(entity =>
        {
            entity.HasKey(e => e.EstadoObjetoId).HasName("PK__EstadoOb__19041DB9EA7F0EBB");

            entity.HasIndex(e => e.NombreEstado, "UQ__EstadoOb__6CE506158EE93AAD").IsUnique();

            entity.Property(e => e.NombreEstado).HasMaxLength(30);
        });

        modelBuilder.Entity<EstadoPago>(entity =>
        {
            entity.HasKey(e => e.EstadoPagoId).HasName("PK__EstadoPa__63AD309D189F1386");

            entity.HasIndex(e => e.NombreEstado, "UQ__EstadoPa__6CE506154DFD9322").IsUnique();

            entity.Property(e => e.NombreEstado).HasMaxLength(20);
        });

        modelBuilder.Entity<EstadoSubasta>(entity =>
        {
            entity.HasKey(e => e.EstadoSubastaId).HasName("PK__EstadoSu__6F18335C190D2CFC");

            entity.HasIndex(e => e.NombreEstado, "UQ__EstadoSu__6CE50615290D5D90").IsUnique();

            entity.Property(e => e.NombreEstado).HasMaxLength(20);
        });

        modelBuilder.Entity<EstadoUsuario>(entity =>
        {
            entity.HasKey(e => e.EstadoUsuarioId).HasName("PK__EstadoUs__BAA0F8A2A7980D4B");

            entity.HasIndex(e => e.NombreEstado, "UQ__EstadoUs__6CE50615882F41E6").IsUnique();

            entity.Property(e => e.NombreEstado).HasMaxLength(20);
        });

        modelBuilder.Entity<Joya>(entity =>
        {
            entity.HasKey(e => e.JoyaId).HasName("PK__Joya__8289A3A1C8DBDBAC");

            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.FechaRegistro)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Nombre).HasMaxLength(120);

            entity.HasOne(d => d.CondicionObjeto).WithMany(p => p.Joya)
                .HasForeignKey(d => d.CondicionObjetoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Joya_Condicion");

            entity.HasOne(d => d.EstadoObjeto).WithMany(p => p.Joya)
                .HasForeignKey(d => d.EstadoObjetoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Joya_Estado");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.Joya)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Joya_Vendedor");

            entity.HasMany(d => d.CategoriaJoya).WithMany(p => p.Joya)
                .UsingEntity<Dictionary<string, object>>(
                    "JoyaCategoria",
                    r => r.HasOne<CategoriaJoya>().WithMany()
                        .HasForeignKey("CategoriaJoyaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_JoyaCategoria_Categoria"),
                    l => l.HasOne<Joya>().WithMany()
                        .HasForeignKey("JoyaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_JoyaCategoria_Joya"),
                    j =>
                    {
                        j.HasKey("JoyaId", "CategoriaJoyaId").HasName("PK__JoyaCate__8D28AD8BFA33D282");
                    });
        });

        modelBuilder.Entity<JoyaImagen>(entity =>
        {
            entity.HasKey(e => e.JoyaImagenId).HasName("PK__JoyaImag__F045507D42C22642");

            entity.Property(e => e.FechaRegistro)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.UrlImagen).HasMaxLength(400);

            entity.HasOne(d => d.Joya).WithMany(p => p.JoyaImagen)
                .HasForeignKey(d => d.JoyaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JoyaImagen_Joya");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.NotificacionId).HasName("PK__Notifica__BCC120246D5DF62F");

            entity.HasIndex(e => new { e.UsuarioId, e.Leida, e.FechaCreacion }, "IX_Notificacion_Usuario_Leida").IsDescending(false, false, true);

            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Mensaje).HasMaxLength(500);
            entity.Property(e => e.Tipo).HasMaxLength(40);

            entity.HasOne(d => d.Subasta).WithMany(p => p.Notificacion)
                .HasForeignKey(d => d.SubastaId)
                .HasConstraintName("FK_Notificacion_Subasta");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Notificacion)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notificacion_Usuario");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.SubastaId).HasName("PK__Pago__46C5CE1AE9B177B3");

            entity.Property(e => e.SubastaId).ValueGeneratedNever();
            entity.Property(e => e.FechaConfirmacion).HasPrecision(0);
            entity.Property(e => e.FechaRegistro)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.EstadoPago).WithMany(p => p.Pago)
                .HasForeignKey(d => d.EstadoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Estado");

            entity.HasOne(d => d.Subasta).WithOne(p => p.Pago)
                .HasForeignKey<Pago>(d => d.SubastaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Subasta");
        });

        modelBuilder.Entity<Puja>(entity =>
        {
            entity.HasKey(e => e.PujaId).HasName("PK__Puja__0F67A0DCBFEE90CA");

            entity.HasIndex(e => new { e.SubastaId, e.FechaHora }, "IX_Puja_Subasta_Fecha").IsDescending(false, true);

            entity.HasIndex(e => new { e.SubastaId, e.MontoOfertado }, "IX_Puja_Subasta_Monto").IsDescending(false, true);

            entity.Property(e => e.FechaHora)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.MontoOfertado).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Comprador).WithMany(p => p.Puja)
                .HasForeignKey(d => d.CompradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Puja_Comprador");

            entity.HasOne(d => d.Subasta).WithMany(p => p.Puja)
                .HasForeignKey(d => d.SubastaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Puja_Subasta");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302F1A81D61AD");

            entity.HasIndex(e => e.NombreRol, "UQ__Rol__4F0B537F843E27D1").IsUnique();

            entity.Property(e => e.NombreRol).HasMaxLength(30);
        });

        modelBuilder.Entity<Subasta>(entity =>
        {
            entity.HasKey(e => e.SubastaId).HasName("PK__Subasta__46C5CE1A567FE639");

            entity.HasIndex(e => new { e.EstadoSubastaId, e.FechaInicio, e.FechaCierre }, "IX_Subasta_Estado_Fechas");

            entity.Property(e => e.FechaCierre).HasPrecision(0);
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FechaInicio).HasPrecision(0);
            entity.Property(e => e.FechaPublicacion).HasPrecision(0);
            entity.Property(e => e.IncrementoMinimo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioBase).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.EstadoSubasta).WithMany(p => p.Subasta)
                .HasForeignKey(d => d.EstadoSubastaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subasta_Estado");

            entity.HasOne(d => d.Joya).WithMany(p => p.Subasta)
                .HasForeignKey(d => d.JoyaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subasta_Joya");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.Subasta)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subasta_Vendedor");
        });

        modelBuilder.Entity<SubastaResultado>(entity =>
        {
            entity.HasKey(e => e.SubastaId).HasName("PK__SubastaR__46C5CE1A8134D790");

            entity.Property(e => e.SubastaId).ValueGeneratedNever();
            entity.Property(e => e.FechaCierre)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.MontoFinal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Ganador).WithMany(p => p.SubastaResultado)
                .HasForeignKey(d => d.GanadorId)
                .HasConstraintName("FK_SubastaResultado_Ganador");

            entity.HasOne(d => d.PujaGanadora).WithMany(p => p.SubastaResultado)
                .HasForeignKey(d => d.PujaGanadoraId)
                .HasConstraintName("FK_SubastaResultado_Puja");

            entity.HasOne(d => d.Subasta).WithOne(p => p.SubastaResultado)
                .HasForeignKey<SubastaResultado>(d => d.SubastaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubastaResultado_Subasta");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8997BA52E");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A194D8AAF51").IsUnique();

            entity.Property(e => e.Correo).HasMaxLength(120);
            entity.Property(e => e.FechaRegistro)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NombreCompleto).HasMaxLength(120);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);

            entity.HasOne(d => d.EstadoUsuario).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.EstadoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Estado");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
