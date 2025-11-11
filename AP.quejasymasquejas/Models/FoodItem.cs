using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.quejasymasquejas.web.Models
{
    [Table("FoodItems", Schema = "dbo")]
    public class FoodItem
    {
        [Key]
        [Column("FoodItemID")]
        public int FoodItemID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)] public string? Category { get; set; }
        [MaxLength(100)] public string? Brand { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        [MaxLength(50)] public string? Unit { get; set; }

        public int? QuantityInStock { get; set; }   // ❌ SIN MaxLength
        public DateTime? ExpirationDate { get; set; }
        public bool? IsPerishable { get; set; }
        public int? CaloriesPerServing { get; set; } // ❌ SIN MaxLength
        public string? Ingredients { get; set; }
        [MaxLength(50)] public string? Barcode { get; set; }
        [MaxLength(100)] public string? Supplier { get; set; }
        public DateTime? DateAdded { get; set; }
        public bool? IsActive { get; set; }

        // FK a Roles (int)
        public int? RoleId { get; set; }            // ❌ SIN MaxLength
        public Role? Role { get; set; }
    }
}
