using api.enums;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDBContext context)
    {
        if (await context.Product.AnyAsync())
            return;

        var products = new List<Product>
        {
            new()
            {
                Name = "Notebook",
                Description = "Notebook para uso profissional",
                UnitValue = 3500.00m
            },
            new()
            {
                Name = "Mouse",
                Description = "Mouse sem fio",
                UnitValue = 120.00m
            },
            new()
            {
                Name = "Teclado",
                Description = "Teclado mecânico",
                UnitValue = 350.00m
            },
            new()
            {
                Name = "Monitor",
                Description = "Monitor 24 polegadas",
                UnitValue = 950.00m
            },
            new()
            {
                Name = "Headset",
                Description = "Headset USB",
                UnitValue = 280.00m
            },
            new()
            {
                Name = "Webcam",
                Description = "Webcam Full HD",
                UnitValue = 220.00m
            },
            new()
            {
                Name = "SSD",
                Description = "SSD 1TB",
                UnitValue = 450.00m
            },
            new()
            {
                Name = "Memória RAM",
                Description = "Memória RAM 16GB",
                UnitValue = 300.00m
            },
            new()
            {
                Name = "Cadeira",
                Description = "Cadeira de escritório",
                UnitValue = 1200.00m
            },
            new()
            {
                Name = "Microfone",
                Description = "Microfone USB",
                UnitValue = 400.00m
            }
        };

        context.Product.AddRange(products);

        await context.SaveChangesAsync();

        var stockAmounts = new[]
        {
            10,
            25,
            8,
            4,
            15,
            0, // produto sem estoque
            12,
            30,
            3,
            7
        };

        var stocks = products
            .Select((product, index) => new Stock
            {
                ProductId = product.Id,
                Product = product,
                Amount = stockAmounts[index]
            })
            .ToList();

        context.Stock.AddRange(stocks);

        var cupons = new List<Cupom>
        {
            new()
            {
                Code = "100FF",
                Type = DiscountType.Percentage,
                Amount = 10m,
                AmountOfUsages = 100,
                ExpiredAt = DateTime.UtcNow.AddYears(1)
            },
            new()
            {
                Code = "150FF",
                Type = DiscountType.Percentage,
                Amount = 15m,
                AmountOfUsages = 100,
                ExpiredAt = DateTime.UtcNow.AddYears(1)
            }
        };

        context.Cupom.AddRange(cupons);

        var movements = products
            .Select((product, index) => new StockMovement
            {
                ProductId = product.Id,
                Product = product,
                PurchaseId = null,
                Type = StockMovementType.inbound,
                Amount = stockAmounts[index]
            })
            .ToList();

        context.StockMovement.AddRange(movements);

        await context.SaveChangesAsync();
    }
}