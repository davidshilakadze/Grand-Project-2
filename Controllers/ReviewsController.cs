using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopBackend.Data;
using OnlineShopBackend.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly OnlineShopContext _context;

        public ReviewsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: api/Reviews/product/5
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsForProduct(int productId)
        {
            var reviews = await _context.Reviews
                                .Where(r => r.ProductId == productId)
                                .ToListAsync();
            return Ok(reviews);
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<IActionResult> PostReview(Review review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Update the product's average rating after adding a review.
            var product = await _context.Products.Include(p => p.Reviews)
                                                .FirstOrDefaultAsync(p => p.Id == review.ProductId);
            if (product != null && product.Reviews.Any())
            {
                product.Rating = product.Reviews.Average(r => r.Rating);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetReviewsForProduct), new { productId = review.ProductId }, review);
        }
    }
}
