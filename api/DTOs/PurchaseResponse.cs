using api.DTOs;
using api.enums;

public class PurchaseResponse
{
    public int Id { get; set; }
    public int ProductsCount { get; set; }
    public decimal Subtotal { get; set; }
    public DiscountType? DiscountType { get; set; }
    public decimal DiscountValue { get; set; }

}