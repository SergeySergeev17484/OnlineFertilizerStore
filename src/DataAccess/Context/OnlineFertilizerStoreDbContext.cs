using DataAccess.Entities;
using DataAccess.Entities.Primitives;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class OnlineFertilizerStoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public OnlineFertilizerStoreDbContext() { }

        public OnlineFertilizerStoreDbContext(DbContextOptions<OnlineFertilizerStoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUser(modelBuilder);
            ConfigureCategory(modelBuilder);
            ConfigureManufacturer(modelBuilder);
            ConfigureProduct(modelBuilder);
            ConfigureCart(modelBuilder);
            ConfigureOrder(modelBuilder);
            ConfigureOrderItem(modelBuilder);
            ConfigureReview(modelBuilder);
        }

        private static void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ExternalId).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FullName)
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasConversion<int>();
            });
        }

        private static void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ExternalId).IsUnique();
                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }

        private static void ConfigureManufacturer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ExternalId).IsUnique();
                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Country)
                    .HasMaxLength(100);
            });
        }

        private static void ConfigureProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ExternalId).IsUnique();
                entity.HasIndex(e => e.CategoryId);
                entity.HasIndex(e => e.ManufacturerId);
                entity.HasIndex(e => e.Price);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(3,2)")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500);

                // Связи
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Manufacturer)
                    .WithMany(m => m.Products)
                    .HasForeignKey(p => p.ManufacturerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureCart(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ExternalId).IsUnique();
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.ProductId);

                // Уникальная комбинация UserId и ProductId
                entity.HasIndex(e => new { e.UserId, e.ProductId })
                    .IsUnique();

                entity.Property(e => e.Quantity)
                    .IsRequired()
                    .HasDefaultValue(1);

                // Связи
                entity.HasOne(c => c.User)
                    .WithMany(u => u.CartItems)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureOrder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ExternalId).IsUnique();
                entity.HasIndex(e => e.OrderNumber).IsUnique();
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.CreationTime);

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasConversion<int>();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasConversion<int>()
                    .HasDefaultValue(OrderStatus.Created);

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                // Связи
                entity.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureOrderItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ExternalId).IsUnique();
                entity.HasIndex(e => e.OrderId);
                entity.HasIndex(e => e.ProductId);

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                // Связи
                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureReview(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ExternalId).IsUnique();
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.ProductId);
                entity.HasIndex(e => e.Rating);

                // Уникальная комбинация UserId и ProductId
                entity.HasIndex(e => new { e.UserId, e.ProductId })
                    .IsUnique();

                entity.Property(e => e.Rating)
                    .IsRequired();

                // Связи
                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}