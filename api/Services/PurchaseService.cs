using api.Data;
using api.DTOs.Common;
using Microsoft.EntityFrameworkCore;

public class PurchaseService
{
    private readonly ApplicationDBContext _context;
    public PurchaseService(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<PurchaseResponse>> GetPagedAsync(int page, int pageSize, DateTime? startDate, DateTime? endDate)
    {
        var query = _context.Purchase.AsNoTracking().AsQueryable();

        var totalItems = await query.CountAsync();

        var purchase = await query
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .Select(p => new PurchaseResponse
            {
                Id = p.Id,
                DiscountValue = p.Discount != null ? p.Discount.Amount : 0,
                DiscountType = p.Discount != null ? p.Discount.Type : null,
                Subtotal = p.Subtotal,
                ProductsCount = p.StockMovements.Sum(sm => sm.Amount)
            })
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<PurchaseResponse>
        {
            Data = purchase,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(
            totalItems / (double)pageSize
        )
        };
    }
    public async Task<DetailedPurchaseResponse?> GetById(int id)
    {
        return await _context.Purchase
        .AsNoTracking()
        .Where(p => p.Id == id)
        .Select(p => new DetailedPurchaseResponse
        {
            Id = p.Id,
            Products = p.StockMovements.Select(sm => new PurchaseProductsResponse
            {
                Id = sm.ProductId,
                Name = sm.Product.Name,
                Amount = sm.Amount,
                Description = sm.Product.Description
            }).ToList()
        }).FirstOrDefaultAsync();

    }
}
