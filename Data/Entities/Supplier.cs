using System.ComponentModel.DataAnnotations;

namespace InventoryManager.Data.Entities;

public class Supplier
{
    public int SupplierId { get; set; }
    
    [Required]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    public string Address { get; set; } = string.Empty;
    
    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
} 