using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AccesoDatos.Modelos
{
    public partial class InmobiliariaContext : DbContext
    {
        public InmobiliariaContext()
        {
        }

        public InmobiliariaContext(DbContextOptions<InmobiliariaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OwnerDb> OwnerDbs { get; set; }
        public virtual DbSet<PropertyDb> PropertyDbs { get; set; }
        public virtual DbSet<PropertyImageDb> PropertyImageDbs { get; set; }
        public virtual DbSet<PropertyTraceDb> PropertyTraceDbs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=GFCOTL-464YDB3\\SQLEXPRESS; Database=Inmobiliaria;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<OwnerDb>(entity =>
            {
                entity.HasKey(e => e.IdOwner)
                    .HasName("PK__Property__D32618169889D1CC");

                entity.ToTable("OwnerDB", "Inmobiliaria");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.OwnerAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PropertyDb>(entity =>
            {
                entity.HasKey(e => e.IdProperty)
                    .HasName("PK__Property__842B6AA75C5612BA");

                entity.ToTable("PropertyDB", "Inmobiliaria");

                entity.Property(e => e.Addres)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CodeInternal)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NameProperty)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(16, 2)");

                entity.HasOne(d => d.IdOwnerNavigation)
                    .WithMany(p => p.PropertyDbs)
                    .HasForeignKey(d => d.IdOwner)
                    .HasConstraintName("FK_Property_PropertyOwner_IdOwner");
            });

            modelBuilder.Entity<PropertyImageDb>(entity =>
            {
                entity.HasKey(e => e.IdPropertyImage)
                    .HasName("PK__Property__018BACD57E7406AA");

                entity.ToTable("PropertyImageDB", "Inmobiliaria");

                entity.Property(e => e.PropertyFile)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyImageDbs)
                    .HasForeignKey(d => d.IdProperty)
                    .HasConstraintName("FK_PropertyImage_Property_IdProperty");
            });

            modelBuilder.Entity<PropertyTraceDb>(entity =>
            {
                entity.HasKey(e => e.IdPropertyTrace)
                    .HasName("PK__Property__373407C91C5A0409");

                entity.ToTable("PropertyTraceDB", "Inmobiliaria");

                entity.Property(e => e.DateSale).HasColumnType("datetime");

                entity.Property(e => e.TraceName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TraceTax).HasColumnType("numeric(16, 2)");

                entity.Property(e => e.TraceValue).HasColumnType("numeric(16, 2)");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyTraceDbs)
                    .HasForeignKey(d => d.IdProperty)
                    .HasConstraintName("FK_PropertyTrace_Property_IdProperty");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
