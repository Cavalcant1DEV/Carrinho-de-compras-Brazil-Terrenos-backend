using api.DTOs.Product;

namespace api.DTOs.Purchase;

public class CreatePurchaseRequest
{
    public int? CupomId { get; set; }
    public ICollection<ProductAmountRequest> Products { get; set; } = [];
}