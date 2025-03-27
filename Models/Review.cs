namespace OnlineShop.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // e.g., 1 to 5 stars
    }
}
