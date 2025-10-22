using Microsoft.EntityFrameworkCore;
using BibliotecaMetropoli.Models;

namespace BibliotecaMetropoli.Data
{
    public class BibliotecaMetropoliContext : DbContext
    {
        public BibliotecaMetropoliContext(DbContextOptions<BibliotecaMetropoliContext> options) : base(options)
        {
        }

        // DbSets con nombres consistentes
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<AutoresRecurso> AutoresRecursos { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<TipoRecurso> TipoRecursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar nombres de tablas explícitamente
            modelBuilder.Entity<Pais>().ToTable("Pais");
            modelBuilder.Entity<Autor>().ToTable("Autor");
            modelBuilder.Entity<AutoresRecurso>().ToTable("AutoresRecursos");
            modelBuilder.Entity<Editorial>().ToTable("Editorial");
            modelBuilder.Entity<Recurso>().ToTable("Recurso");
            modelBuilder.Entity<TipoRecurso>().ToTable("TipoRecurso");

            // Configurar AutoresRecurso con clave compuesta
            modelBuilder.Entity<AutoresRecurso>()
                .HasKey(ar => new { ar.IdRec, ar.idAutor });

            // Relaciones para AutoresRecurso
            modelBuilder.Entity<AutoresRecurso>()
                .HasOne(ar => ar.Recurso)
                .WithMany(r => r.AutoresRecursos)
                .HasForeignKey(ar => ar.IdRec)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AutoresRecurso>()
                .HasOne(ar => ar.Autor)
                .WithMany(a => a.AutoresRecursos)
                .HasForeignKey(ar => ar.idAutor)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar Recurso - SOLO estas relaciones
            modelBuilder.Entity<Recurso>(entity =>
            {
                entity.HasKey(e => e.IdRec);

                // Propiedades
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(255);
                entity.Property(e => e.annopublic).IsRequired(false);
                entity.Property(e => e.Edicion).HasMaxLength(50).IsRequired(false);
                entity.Property(e => e.PalabrasBusqueda).HasMaxLength(500).IsRequired(false);
                entity.Property(e => e.Descripcion).HasMaxLength(1000).IsRequired(false);

                // SOLO estas 3 relaciones
                entity.HasOne(r => r.Pais)
                    .WithMany(p => p.Recursos)
                    .HasForeignKey(r => r.IdPais)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.TipoRecurso)
                    .WithMany(tr => tr.Recursos)
                    .HasForeignKey(r => r.IdTipoR)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Editorial)
                    .WithMany(e => e.Recursos)
                    .HasForeignKey(r => r.IdEdit)
                    .OnDelete(DeleteBehavior.Restrict);

                // ⚠️ NO configurar relación con Autor aquí
            });

            // Configurar Autor
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.HasKey(e => e.idAutor);
                entity.Property(e => e.Nombres).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellidos).IsRequired().HasMaxLength(100);
            });

            // Configurar las demás entidades
            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.IdPais);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Editorial>(entity =>
            {
                entity.HasKey(e => e.IdEdit);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500).IsRequired(false);
            });

            modelBuilder.Entity<TipoRecurso>(entity =>
            {
                entity.HasKey(e => e.IdTipoR);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500).IsRequired(false);
            });

            // Índices
            modelBuilder.Entity<Recurso>()
                .HasIndex(r => r.IdPais);
            modelBuilder.Entity<Recurso>()
                .HasIndex(r => r.IdTipoR);
            modelBuilder.Entity<Recurso>()
                .HasIndex(r => r.IdEdit);
            modelBuilder.Entity<AutoresRecurso>()
                .HasIndex(ar => ar.idAutor);
        }
    }
}