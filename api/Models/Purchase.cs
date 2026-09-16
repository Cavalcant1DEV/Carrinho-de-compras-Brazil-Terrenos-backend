using System.ComponentModel.DataAnnotations.Schema;

namespace api.models
{
    public class Purchase
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        public int? CupomId { get; set; }
        public Cupom? Cupom { get; set; }
        public ICollection<StockMovement> StockMovements { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}