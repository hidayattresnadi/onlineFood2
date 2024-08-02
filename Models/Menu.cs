using System.ComponentModel.DataAnnotations;

namespace OnlineFoodWebAPI.Models
{
    public class Menu
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters long.")]
        public string Name { get; set; }
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100000")]
        public decimal Price { get; set; }
        [AllowedValues("Food", "Beverage", "Dessert", ErrorMessage = "Only 3 categories are allowed :Food, Beverage, and Dessert")]
        public string Category { get; set; }
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public decimal Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAvailable { get; set; }
        public List<int> RatingCustomers { get; set; } = new List<int>();
    }

}