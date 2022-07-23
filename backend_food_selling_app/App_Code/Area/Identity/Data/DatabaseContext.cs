using System;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Summary description for DbContext
/// </summary>
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
    }

    public DbSet<BillEntity> Bill { get; set; }
}