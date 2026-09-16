namespace api.DTOs.Purchase;

using api.enums;
public class DetailedPurchaseResponse
{
    public int Id { get; set; }
    public int ProductsCount { get; set; }
    public decimal Subtotal { get; set; }
    public DiscountType? DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public ICollection<PurchaseProductsResponse> Products { get; set; } = [];
}