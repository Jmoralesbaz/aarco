using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Examen.Marcas.Data
{
    public partial class ContextExamen : DbContext
    {
        public ContextExamen()
        {
        }

        public ContextExamen(DbContextOptions<ContextExamen> options)
            : base(options)
        {
        }

        public virtual DbSet<CatDescripciones> CatDescripciones { get; set; }
        public virtual DbSet<CatModelos> CatModelos { get; set; }
        public virtual DbSet<CatSubMarcas> CatSubMarcas { get; set; }
        public virtual DbSet<Datos> Datos { get; set; }
        public virtual DbSet<Descripciones> Descripciones { get; set; }
        public virtual DbSet<Marcas> Marcas { get; set; }
        public virtual DbSet<Modelos> Modelos { get; set; }
        public virtual DbSet<SubMarcas> SubMarcas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=TI1\\MSSQLTIS01;Database=Examen_Aarco;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatDescripciones>(entity =>
            {
                entity.ToTable("Cat_Descripciones");

                entity.HasIndex(e => e.DescripcionId)
                    .HasName("UQ__Cat_Desc__612EA49EEDCD7CFC")
                    .IsUnique();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DescripcionId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CatModelos>(entity =>
            {
                entity.ToTable("Cat_Modelos");

                entity.HasIndex(e => e.Modelo)
                    .HasName("UQ__Cat_Mode__D83E61C011330E04")
                    .IsUnique();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CatSubMarcas>(entity =>
            {
                entity.ToTable("Cat_SubMarcas");

                entity.HasIndex(e => e.SubMarca)
                    .HasName("UQ__Cat_SubM__94F4D0E5588AAE18")
                    .IsUnique();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubMarca)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Datos>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.DescripcionId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Submarca)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Descripciones>(entity =>
            {
                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.DescripcionNavigation)
                    .WithMany(p => p.Descripciones)
                    .HasForeignKey(d => d.Descripcion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Descripci__Descr__29572725");

                entity.HasOne(d => d.ModeloNavigation)
                    .WithMany(p => p.Descripciones)
                    .HasForeignKey(d => d.Modelo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Descripci__Model__2A4B4B5E");

                entity.HasOne(d => d.SubMarcaNavigation)
                    .WithMany(p => p.Descripciones)
                    .HasForeignKey(d => d.SubMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Descripci__SubMa__2B3F6F97");
            });

            modelBuilder.Entity<Marcas>(entity =>
            {
                entity.HasIndex(e => e.Marca)
                    .HasName("UQ__Marcas__32E2BA4ED1477A86")
                    .IsUnique();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Modelos>(entity =>
            {
                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ModeloNavigation)
                    .WithMany(p => p.Modelos)
                    .HasForeignKey(d => d.Modelo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Modelos__Modelo__25869641");

                entity.HasOne(d => d.SubMarcaNavigation)
                    .WithMany(p => p.Modelos)
                    .HasForeignKey(d => d.SubMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Modelos__SubMarc__24927208");
            });

            modelBuilder.Entity<SubMarcas>(entity =>
            {
                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.MarcaNavigation)
                    .WithMany(p => p.SubMarcas)
                    .HasForeignKey(d => d.Marca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubMarcas__Marca__1FCDBCEB");

                entity.HasOne(d => d.SubMarcaNavigation)
                    .WithMany(p => p.SubMarcas)
                    .HasForeignKey(d => d.SubMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubMarcas__SubMa__20C1E124");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
