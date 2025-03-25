using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopBackend.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        // Average rating based on reviews
        public double Rating { get; set; }

        // Filtering properties
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; }

        // Navigation property for reviews
        public ICollection<Review> Reviews { get; set; }
    }
}
