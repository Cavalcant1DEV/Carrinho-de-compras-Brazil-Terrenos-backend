using api.enums;
using Microsoft.Net.Http.Headers;

namespace api.models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int UserId { get; set; } = 1;
        public int PurchaseId { get; set; }
        public Purchase? Purchase { get; set; }
        public int ProductId { get; set; }
        public required Product Product { get; set; }
        public StockMovementType Type { get; set; }
        public int Amount { get; set; }
        public Discount? Discount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}