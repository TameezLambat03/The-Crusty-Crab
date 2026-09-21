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

    public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Cart> Carts { get; set; }

    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Staff> Staff { get; set; }


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

        modelBuilder.Entity<DeliveryAddress>()
            .ToTable("Delivery_Address");

        modelBuilder.Entity<DeliveryAddress>()
            .HasKey(a => a.DeliveryAddressID);

        modelBuilder.Entity<DeliveryAddress>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(a => a.CustomerID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MenuItem>()
    .ToTable("Menu_Item");

        modelBuilder.Entity<MenuItem>()
            .HasKey(m => m.MenuItemID);

        modelBuilder.Entity<Cart>()
    .ToTable("Cart");

        modelBuilder.Entity<Cart>()
            .HasKey(c => c.CartID);

        modelBuilder.Entity<Cart>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(c => c.CustomerID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .ToTable("Cart_Item");

        modelBuilder.Entity<CartItem>()
            .HasKey(ci => ci.CartItemID);

        modelBuilder.Entity<CartItem>()
            .HasOne<Cart>()
            .WithMany()
            .HasForeignKey(ci => ci.CartID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .HasOne<MenuItem>()
            .WithMany()
            .HasForeignKey(ci => ci.MenuItemID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
    .ToTable("Order");

        modelBuilder.Entity<Order>()
            .HasKey(o => o.OrderID);

        modelBuilder.Entity<Order>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne<DeliveryAddress>()
            .WithMany()
            .HasForeignKey(o => o.DeliveryAddressID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne<Staff>()
            .WithMany()
            .HasForeignKey(o => o.StaffId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<OrderItem>()
            .ToTable("Order_Item");

        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => oi.OrderItemID);

        modelBuilder.Entity<OrderItem>()
            .HasOne<Order>()
            .WithMany()
            .HasForeignKey(oi => oi.OrderID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne<MenuItem>()
            .WithMany()
            .HasForeignKey(oi => oi.MenuItemID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Staff>()
    .ToTable("Staff");

        modelBuilder.Entity<Staff>()
            .HasKey(s => s.StaffId);
    }
}