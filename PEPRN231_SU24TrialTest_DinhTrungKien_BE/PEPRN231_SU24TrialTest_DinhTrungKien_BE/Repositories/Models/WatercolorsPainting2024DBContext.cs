using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models
{
    public partial class WatercolorsPainting2024DBContext : DbContext
    {
        public WatercolorsPainting2024DBContext()
        {
        }

        public WatercolorsPainting2024DBContext(DbContextOptions<WatercolorsPainting2024DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Style> Styles { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
        public virtual DbSet<WatercolorsPainting> WatercolorsPaintings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(local);Database=WatercolorsPainting2024DB;Uid=sa;Pwd=12345;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Style>(entity =>
            {
                entity.ToTable("Style");

                entity.Property(e => e.StyleId).HasMaxLength(30);

                entity.Property(e => e.OriginalCountry).HasMaxLength(160);

                entity.Property(e => e.StyleDescription).HasMaxLength(250);

                entity.Property(e => e.StyleName).HasMaxLength(100);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount");

                entity.HasIndex(e => e.UserEmail, "UQ__UserAcco__08638DF8672AFFCA")
                    .IsUnique();

                entity.Property(e => e.UserAccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserAccountID");

                entity.Property(e => e.UserEmail).HasMaxLength(90);

                entity.Property(e => e.UserFullName).HasMaxLength(70);

                entity.Property(e => e.UserPassword).HasMaxLength(50);
            });

            modelBuilder.Entity<WatercolorsPainting>(entity =>
            {
                entity.HasKey(e => e.PaintingId)
                    .HasName("PK__Watercol__CF2D90F2F6C5EE6D");

                entity.ToTable("WatercolorsPainting");

                entity.Property(e => e.PaintingId).HasMaxLength(45);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PaintingAuthor).HasMaxLength(120);

                entity.Property(e => e.PaintingDescription).HasMaxLength(250);

                entity.Property(e => e.PaintingName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StyleId).HasMaxLength(30);

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.WatercolorsPaintings)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Watercolo__Style__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
