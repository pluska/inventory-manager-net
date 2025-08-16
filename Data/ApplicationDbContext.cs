using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InventoryManager.Data.Entities;

namespace InventoryManager.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure Product entity
        builder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.SKU).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.HasIndex(e => e.SKU).IsUnique();
            
            // Relationship with Supplier
            entity.HasOne(e => e.Supplier)
                  .WithMany(e => e.Products)
                  .HasForeignKey(e => e.SupplierId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure Supplier entity
        builder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configure StockMovement entity
        builder.Entity<StockMovement>(entity =>
        {
            entity.HasKey(e => e.MovementId);
            entity.Property(e => e.Timestamp).HasDefaultValueSql("GETUTCDATE()");
            
            // Relationship with Product
            entity.HasOne(e => e.Product)
                  .WithMany(e => e.StockMovements)
                  .HasForeignKey(e => e.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // UserId is just a string field, no foreign key relationship

        });
    }
    
    public void SeedData()
    {
        // Only seed if no data exists
        if (Products.Any() || Suppliers.Any())
            return;
            
        // Add sample suppliers
        var supplier1 = new Supplier
        {
            Name = "Tech Supplies Inc.",
            Email = "info@techsupplies.com",
            Phone = "+1-555-0123",
            Address = "123 Tech Street, Silicon Valley, CA 94025"
        };
        
        var supplier2 = new Supplier
        {
            Name = "Office Depot",
            Email = "orders@officedepot.com",
            Phone = "+1-555-0456",
            Address = "456 Office Avenue, Business District, NY 10001"
        };
        
        Suppliers.AddRange(supplier1, supplier2);
        SaveChanges();
        
        // Add sample products
        var product1 = new Product
        {
            Name = "Wireless Mouse",
            SKU = "WM-001",
            Price = 29.99m,
            Stock = 50,
            MinimumStock = 10,
            SupplierId = supplier1.SupplierId
        };
        
        var product2 = new Product
        {
            Name = "USB Keyboard",
            SKU = "KB-002",
            Price = 49.99m,
            Stock = 30,
            MinimumStock = 15,
            SupplierId = supplier1.SupplierId
        };
        
        var product3 = new Product
        {
            Name = "Printer Paper",
            SKU = "PP-003",
            Price = 9.99m,
            Stock = 200,
            MinimumStock = 50,
            SupplierId = supplier2.SupplierId
        };
        
        Products.AddRange(product1, product2, product3);
        SaveChanges();
        
        // Add sample stock movements
        var movement1 = new StockMovement
        {
            MovementType = MovementType.IN,
            ProductId = product1.ProductId,
            Quantity = 50,
            Timestamp = DateTime.UtcNow.AddDays(-7),
            UserId = "system"
        };
        
        var movement2 = new StockMovement
        {
            MovementType = MovementType.OUT,
            ProductId = product1.ProductId,
            Quantity = 5,
            Timestamp = DateTime.UtcNow.AddDays(-1),
            UserId = "system"
        };
        
        StockMovements.AddRange(movement1, movement2);
        SaveChanges();
    }
} 