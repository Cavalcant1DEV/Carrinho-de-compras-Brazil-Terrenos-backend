using System.ComponentModel.DataAnnotations.Schema;
using api.enums;

namespace api.models
{
    public class Cupom
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DiscountType type { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        public int AmountOfUsages { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime expiredAt { get; set; }
    }
}