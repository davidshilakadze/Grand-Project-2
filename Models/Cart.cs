namespace OnlineShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        // This could be tied to a User, e.g. public string UserId { get; set; }
        public ICollection<CartItem> Items { get; set; }
    }
}
