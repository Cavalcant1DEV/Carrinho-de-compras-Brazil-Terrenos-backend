using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ProductController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string? name = null
        )
        {
            if (page < 1)
                return BadRequest("Page must be greater than 0.");

            if (pageSize < 1 || pageSize > 100)
                return BadRequest("PageSize must be between 1 and 100.");

            var query = _context.Product.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));
            }

            var totalItems = await query.CountAsync();

            var products = await query
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    StockAmount = p.Stock != null ? p.Stock.Amount : 0
                })
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                data = products,
                pagination = new
                {
                    page,
                    pageSize,
                    totalItems,
                    totalPages = (int)Math.Ceiling(
                        totalItems / (double)pageSize
                    )
                }
            });
        }
    }
}