namespace api.DTOs.Purchase;

using api.enums;
public class PurchaseProductsResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public int Amount { get; set; }

    public decimal UnitValue { get; set; }

}