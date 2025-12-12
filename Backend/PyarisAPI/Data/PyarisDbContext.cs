using Microsoft.EntityFrameworkCore;
using PyarisAPI.Models;

namespace PyarisAPI.Data
{
    public class PyarisDbContext : DbContext
    {
        public PyarisDbContext(DbContextOptions<PyarisDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("XMaster Menu");
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.MenuName).HasColumnName("Menu Name").HasMaxLength(255);
                entity.Property(e => e.Group).HasMaxLength(100);
                entity.Property(e => e.SubGroup).HasMaxLength(100);
            });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.CustomerId).HasMaxLength(50);
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.Property(e => e.PaymentMethod).HasMaxLength(50);
                entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            });

            // Configure OrderItem entity
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.OrderItemId);
                entity.HasOne<Order>().WithMany().HasForeignKey(e => e.OrderId);
                entity.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductId);
            });

            // Configure Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Pincode).HasMaxLength(10);
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Role).HasMaxLength(50);
            });

            // Configure PaymentTransaction entity
            modelBuilder.Entity<PaymentTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);
                entity.Property(e => e.PaymentGateway).HasMaxLength(50);
                entity.Property(e => e.Status).HasMaxLength(50);
            });

            // Configure Refund entity
            modelBuilder.Entity<Refund>(entity =>
            {
                entity.HasKey(e => e.RefundId);
                entity.Property(e => e.Status).HasMaxLength(50);
            });
        }
    }
}
