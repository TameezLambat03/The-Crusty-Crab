using CrustyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrustyAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<AdminUser> AdminUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .ToTable("Customer");

        modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerID);

        modelBuilder.Entity<AdminUser>()
            .ToTable("Admin_User");

        modelBuilder.Entity<AdminUser>()
            .HasKey(a => a.AdminID);
    }
}