using System;
using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100, ErrorMessage = "Item name cannot exceed 100 characters.")]
        public string? ItemName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 99999.99, ErrorMessage = "Price must be between 0.01 and 99999.99.")]
        public decimal Price { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Required(ErrorMessage = "Customer email is required.")]
        public string? CustomerEmail { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}
