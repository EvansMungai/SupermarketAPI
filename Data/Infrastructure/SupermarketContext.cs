using Microsoft.EntityFrameworkCore;
using Supermarket.API.Features.Authentication.Models;
using Supermarket.API.Features.BranchManagement.Models;
using Supermarket.API.Features.DrinkManagement.Models;
using Supermarket.API.Features.InventoryManagement.Models;
using Supermarket.API.Features.PaymentManagement.Models;
using Supermarket.API.Features.SalesManagement.Models;

namespace Supermarket.API.Data.Infrastructure;

public partial class SupermarketContext : DbContext
{
    public SupermarketContext()
    {
    }

    public SupermarketContext(DbContextOptions<SupermarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=DefaultConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PRIMARY");

            entity.ToTable("branches");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .HasColumnName("branch_name");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .HasColumnName("location");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.DrinkId).HasName("PRIMARY");

            entity.ToTable("drinks");

            entity.Property(e => e.DrinkId).HasColumnName("drink_id");
            entity.Property(e => e.DrinkName)
                .HasMaxLength(30)
                .HasColumnName("drink_name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PRIMARY");

            entity.ToTable("inventory");

            entity.HasIndex(e => e.BranchId, "branch_id");

            entity.HasIndex(e => e.DrinkId, "drink_id");

            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.DrinkId).HasColumnName("drink_id");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

            entity.HasOne(d => d.Branch).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("inventory_ibfk_1");

            entity.HasOne(d => d.Drink).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("inventory_ibfk_2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PRIMARY");

            entity.ToTable("payments");

            entity.HasIndex(e => e.SaleId, "sale_id");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasColumnName("status");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .HasColumnName("transaction_id");

            entity.HasOne(d => d.Sale).WithMany(p => p.Payments)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("payments_ibfk_1");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PRIMARY");

            entity.ToTable("sales");

            entity.HasIndex(e => e.BranchId, "branch_id");

            entity.HasIndex(e => e.DrinkId, "drink_id");

            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DrinkId).HasColumnName("drink_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.UserId)
                .HasMaxLength(256)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Branch).WithMany(p => p.Sales)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("sales_ibfk_1");

            entity.HasOne(d => d.Drink).WithMany(p => p.Sales)
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("sales_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserName, "UserName").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(30)
                .HasColumnName("role");
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
