using System;
using Microsoft.EntityFrameworkCore;
public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=android;port=3306;user=root;password=;charset=utf8mb4;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BillEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Voucher);
            entity.Property(e => e.CustomerId).IsRequired();
            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.PaymentMethod).IsRequired();
            entity.Property(e => e.Status).IsRequired();
        });

        modelBuilder.Entity<BillDetailsEntity>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.FoodId);
            entity.Property(e => e.BillId);
            entity.Property(e => e.Amount);
        });

        modelBuilder.Entity<CustomerEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Address);
            entity.Property(e => e.Phone).IsRequired();
            entity.Property(e => e.Username);
        });

        modelBuilder.Entity<SaleEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FoodType).IsRequired();
            entity.Property(e => e.Rate).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            entity.Property(e => e.Description);
            entity.Property(e => e.Active).IsRequired();
        });

        modelBuilder.Entity<VoucherEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Rate).IsRequired();
            entity.Property(e => e.Active).IsRequired();
        });
    }

    public DbSet<BillEntity> Bill { get; set; }
    public DbSet<BillDetailsEntity> Bill_Details { get; set; }
    public DbSet<CustomerEntity> Customer { get; set; }
    public DbSet<SaleEntity> Sale { get; set; }
    public DbSet<VoucherEntity> Voucher { get; set; }
}