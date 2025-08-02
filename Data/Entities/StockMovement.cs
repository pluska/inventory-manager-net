using System.ComponentModel.DataAnnotations;

namespace InventoryManager.Data.Entities;

public enum MovementType
{
    IN,
    OUT,
    ADJUSTMENT
}

public class StockMovement
{
    public int MovementId { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }
    
    [Required]
    public MovementType MovementType { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    // Navigation properties
    public virtual Product Product { get; set; } = null!;
    public virtual Microsoft.AspNetCore.Identity.IdentityUser User { get; set; } = null!;
} 