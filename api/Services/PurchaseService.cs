using api.Data;
using api.DTOs.Common;
using api.DTOs.Purchase;
using api.enums;
using api.models;
using Microsoft.AspNetCore.Mvc;
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
                DiscountValue = p.Cupom != null ? p.Cupom.Amount : 0,
                DiscountType = p.Cupom != null ? p.Cupom.Type : null,
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

    public async Task<CreatePurchaseResponse> CreatePurchase(CreatePurchaseRequest request)
    {
        await using var transaction =
        await _context.Database.BeginTransactionAsync();

        try
        {
            var productIds = request.Products
                .Select(p => p.Id)
                .ToList();

            var products = await _context.Product
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

            var subtotal = request.Products.Sum(item =>
            {
                var product = products.First(p => p.Id == item.Id);

                return product.UnitValue * item.Amount;
            });

            var purchase = new Purchase
            {
                CupomId = request.CupomId,
                Subtotal = subtotal
            };

            _context.Purchase.Add(purchase);

            foreach (var item in request.Products)
            {
                var product = await _context.Product.Include(p => p.Stock).FirstOrDefaultAsync(p => p.Id == item.Id);

                if (product == null)
                    throw new ArgumentException(
                        $"Produto {item.Id} não encontrado em estoque."
                    );

                if (product.Stock == null || product.Stock.Amount < item.Amount)
                {
                    throw new ArgumentException(
                        $"Estoque insuficiente para {product.Name}."
                    );
                }

                product.Stock.Amount -= item.Amount;

                var movement = new StockMovement
                {
                    Amount = item.Amount,
                    ProductId = item.Id,
                    Type = StockMovementType.outbound,
                    Purchase = purchase
                };

                _context.StockMovement.Add(movement);
            }

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new CreatePurchaseResponse
            {
                Id = purchase.Id
            };

        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
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
