using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblSale> TblSales { get; set; }

    public virtual DbSet<TblSaleDetail> TblSaleDetails { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        string connectionString = "Server=.;Database=PointOfSale;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";
    //        optionsBuilder.UseSqlServer(connectionString);
    //    }
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Tbl_Cate__19093A2B41B0C086");

            entity.ToTable("Tbl_Category", tb => tb.HasTrigger("trg_GenerateCategoryCode"));

            entity.HasIndex(e => e.CategoryCode, "UQ__Tbl_Cate__371BA9553AFEB3DC").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryCode).HasMaxLength(4);
            entity.Property(e => e.CategoryName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Tbl_Prod__B40CC6CDB44D1EE5");

            entity.ToTable("Tbl_Product", tb => tb.HasTrigger("trg_GenerateProductCode"));

            entity.HasIndex(e => e.ProductCode, "UQ__Tbl_Prod__2F4E024FB862BCE1").IsUnique();

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductCategoryCode).HasMaxLength(4);
            entity.Property(e => e.ProductCode).HasMaxLength(4);
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductCategoryCodeNavigation).WithMany(p => p.TblProducts)
                .HasPrincipalKey(p => p.CategoryCode)
                .HasForeignKey(d => d.ProductCategoryCode)
                .HasConstraintName("FK__Tbl_Produ__Produ__3C69FB99");
        });

        modelBuilder.Entity<TblSale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Tbl_Sale__1EE3C3FF50381598");

            entity.ToTable("Tbl_Sale", tb => tb.HasTrigger("trg_GenerateSaleCode"));

            entity.HasIndex(e => e.SaleCode, "UQ__Tbl_Sale__0F57A49F7A783FB1").IsUnique();

            entity.Property(e => e.ChangeAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PayAmount).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.SaleCode).HasMaxLength(4);
            entity.Property(e => e.SaleDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalSale).HasColumnType("decimal(15, 2)");
        });

        modelBuilder.Entity<TblSaleDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__Tbl_Sale__135C316D2C0D4F30");

            entity.ToTable("Tbl_SaleDetail", tb => tb.HasTrigger("trg_GenerateSaleDetailCode"));

            entity.Property(e => e.DetailCode).HasMaxLength(4);
            entity.Property(e => e.ProductCode).HasMaxLength(4);
            entity.Property(e => e.SaleCode).HasMaxLength(4);
            entity.Property(e => e.Total).HasColumnType("decimal(15, 2)");

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.TblSaleDetails)
                .HasPrincipalKey(p => p.ProductCode)
                .HasForeignKey(d => d.ProductCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_SaleD__Produ__48CFD27E");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.TblSaleDetails)
                .HasPrincipalKey(p => p.SaleCode)
                .HasForeignKey(d => d.SaleCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_SaleD__SaleC__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
