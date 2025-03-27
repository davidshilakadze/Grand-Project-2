using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/cart/1
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(int cartId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // PUT: api/cart/update-item
        [HttpPut("update-item")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItem cartItem)
        {
            var existingItem = await _context.CartItems.FindAsync(cartItem.Id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Quantity = cartItem.Quantity;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/cart/delete-item/5
        [HttpDelete("delete-item/{itemId}")]
        public async Task<IActionResult> DeleteCartItem(int itemId)
        {
            var item = await _context.CartItems.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
