using System.ComponentModel.DataAnnotations;

namespace InventoryManager.Data.Entities;

public class Product
{
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50, ErrorMessage = "SKU cannot exceed 50 characters")]
    public string SKU { get; set; } = string.Empty;
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
    public decimal Price { get; set; }
    
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than or equal to 0")]
    public int Stock { get; set; }
    
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Minimum stock must be greater than or equal to 0")]
    public int MinimumStock { get; set; }
    
    public int? SupplierId { get; set; }
    
    // Navigation properties
    public virtual Supplier? Supplier { get; set; }
    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    
    // Computed property for low stock warning
    public bool IsLowStock => Stock <= MinimumStock;
} 