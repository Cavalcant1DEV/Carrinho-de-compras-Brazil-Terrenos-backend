using api.DTOs;

public class ProductResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public int? StockAmount { get; set; } = 0;
}