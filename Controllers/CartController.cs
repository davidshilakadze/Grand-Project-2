using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopBackend.Data;
using OnlineShopBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly OnlineShopContext _context;

        public CartController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: api/Cart
        // Retrieves the current cart (for simplicity, a single cart instance is assumed).
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _context.Carts
                                .Include(c => c.Items)
                                .ThenInclude(i => i.Product)
                                .FirstOrDefaultAsync();
            if (cart == null)
            {
                cart = new Cart { Items = new List<CartItem>() };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }
            return Ok(cart);
        }

        // POST: api/Cart/add
        // Adds a product to the cart.
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem cartItem)
        {
            var cart = await _context.Carts
                                .Include(c => c.Items)
                                .FirstOrDefaultAsync();
            if (cart == null)
            {
                cart = new Cart { Items = new List<CartItem>() };
                _context.Carts.Add(cart);
            }

            // Check if the product already exists in the cart.
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == cartItem.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
            }
            else
            {
                // Ensure the new item is linked to the cart.
                cartItem.CartId = cart.Id;
                cart.Items.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return Ok(cart);
        }

        // PUT: api/Cart/update/{itemId}
        // Updates the quantity of a cart item.
        [HttpPut("update/{itemId}")]
        public async Task<IActionResult> UpdateCartItem(int itemId, [FromBody] int quantity)
        {
            var item = await _context.CartItems.FindAsync(itemId);
            if (item == null)
                return NotFound();

            item.Quantity = quantity;
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // DELETE: api/Cart/remove/{itemId}
        // Removes an item from the cart.
        [HttpDelete("remove/{itemId}")]
        public async Task<IActionResult> RemoveCartItem(int itemId)
        {
            var item = await _context.CartItems.FindAsync(itemId);
            if (item == null)
                return NotFound();

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
