using api.Controllers;
using api.Data;
using api.DTOs.Common;
using api.DTOs.Cupom;
using Microsoft.EntityFrameworkCore;

public class CupomService
{
    private readonly ApplicationDBContext _context;

    public CupomService(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<CupomResponse?> GetCupomAsync(string code)
    {
        var cupom = await _context.Cupom
            .AsNoTracking()
            .Where(c => c.Code == code)
            .Select(c => new CupomResponse
            {
                Id = c.Id,
                Type = c.Type,
                Value = c.Amount,
                AmountOfUsages = c.AmountOfUsages,
                ExpiredAt = c.ExpiredAt
            })
            .FirstOrDefaultAsync();

        if (cupom != null)
        {
            if (cupom.ExpiredAt != null)
                throw new ArgumentException($"Cupom Expirado!");

            if (cupom.AmountOfUsages <= 0)
                throw new ArgumentException($"Cupom esgotado!");

        }

        return cupom;
    }
}
