using System.ComponentModel.DataAnnotations.Schema;

namespace api.models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal totalDiscount { get; set; }
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
        public ICollection<StockMovement> StockMovements { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}