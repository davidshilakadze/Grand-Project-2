using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopBackend.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        // Reviewer’s full name (first and last)
        [Required]
        public string ReviewerName { get; set; }

        // Rating (e.g., 1 to 5 stars)
        [Range(1, 5)]
        public int Rating { get; set; }

        // Textual review comment
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public Product Product { get; set; }
    }
}
