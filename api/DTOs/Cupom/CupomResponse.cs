using api.enums;

namespace api.DTOs.Cupom;

public class CupomResponse
{
    public int Id { get; set; }
    public DiscountType Type { get; set; }
    public decimal Value { get; set; }
    public int AmountOfUsages { get; set; }
    public DateTime? ExpiredAt { get; set; }
}