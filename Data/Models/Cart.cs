using System.Collections.Generic;

namespace OnlineShopBackend.Models
{
    public class Cart
    {
        public int Id { get; set; }

        // You can later associate this with a specific User
        public ICollection<CartItem> Items { get; set; }
    }
}
