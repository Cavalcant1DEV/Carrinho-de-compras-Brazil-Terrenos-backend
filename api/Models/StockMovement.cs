using api.enums;

namespace api.models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int UserId { get; set; } = 1;
        public int ProductId { get; set; }
        public ICollection<Product> Products { get; set; } = [];
        public StockMovementType Type { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}