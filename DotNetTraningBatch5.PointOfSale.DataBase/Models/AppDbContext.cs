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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = .;Initial Catalog = PointOfSale;User ID =sa; Password = sasa@123;TrustServerCertificate  = True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Tbl_Cate__19093A2BF9E2C771");

            entity.ToTable("Tbl_Category", tb => tb.HasTrigger("trg_GenerateCategoryCode"));

            entity.HasIndex(e => e.CategoryCode, "UQ__Tbl_Cate__371BA9559A75705C").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryCode).HasMaxLength(4);
            entity.Property(e => e.CategoryName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Tbl_Prod__B40CC6CD68C73B9C");

            entity.ToTable("Tbl_Product", tb => tb.HasTrigger("trg_GenerateProductCode"));

            entity.HasIndex(e => e.ProductCode, "UQ__Tbl_Prod__2F4E024FC7EBFA56").IsUnique();

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductCategoryCode).HasMaxLength(4);
            entity.Property(e => e.ProductCode).HasMaxLength(4);
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductCategoryCodeNavigation).WithMany(p => p.TblProducts)
                .HasPrincipalKey(p => p.CategoryCode)
                .HasForeignKey(d => d.ProductCategoryCode)
                .HasConstraintName("FK__Tbl_Produ__Produ__7A672E12");
        });

        modelBuilder.Entity<TblSale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Tbl_Sale__1EE3C3FF62DFABC9");

            entity.ToTable("Tbl_Sale", tb => tb.HasTrigger("trg_GenerateSaleCode"));

            entity.HasIndex(e => e.SaleCode, "UQ__Tbl_Sale__0F57A49FED54C552").IsUnique();

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
            entity.HasKey(e => e.DetailId).HasName("PK__Tbl_Sale__135C316D49DA3771");

            entity.ToTable("Tbl_SaleDetail", tb => tb.HasTrigger("trg_GenerateSaleDetailCode"));

            entity.Property(e => e.DetailCode).HasMaxLength(4);
            entity.Property(e => e.ProductCode).HasMaxLength(4);
            entity.Property(e => e.SaleCode).HasMaxLength(4);
            entity.Property(e => e.Total).HasColumnType("decimal(15, 2)");

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.TblSaleDetails)
                .HasPrincipalKey(p => p.ProductCode)
                .HasForeignKey(d => d.ProductCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_SaleD__Produ__02FC7413");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.TblSaleDetails)
                .HasPrincipalKey(p => p.SaleCode)
                .HasForeignKey(d => d.SaleCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_SaleD__SaleC__03F0984C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
