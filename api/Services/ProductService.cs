using api.Data;
using api.DTOs.Common;
using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private readonly ApplicationDBContext _context;

    public ProductService(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<ProductResponse>> GetPagedAsync(int page, int pageSize, string? name)
    {
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

        return new PagedResponse<ProductResponse>
        {
            Data = products,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(
            totalItems / (double)pageSize
        )
        };
    }
}
