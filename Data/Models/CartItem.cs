namespace OnlineShopBackend.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        // Foreign key and navigation property for the parent Cart
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        // Foreign key and navigation property for the associated Product
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
