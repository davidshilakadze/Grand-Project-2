using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/products?categoryId=1&subCategoryId=2
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int? categoryId, [FromQuery] int? subCategoryId)
        {
            var query = _context.Products
                .Include(p => p.SubCategory)
                .ThenInclude(sc => sc.Category)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.SubCategory.CategoryId == categoryId.Value);
            }
            if (subCategoryId.HasValue)
            {
                query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
            }

            var products = await query.ToListAsync();
            return Ok(products);
        }
    }
}
