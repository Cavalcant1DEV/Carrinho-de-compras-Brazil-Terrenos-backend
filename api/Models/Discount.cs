using System.ComponentModel.DataAnnotations.Schema;
using api.enums;

namespace api.models
{
    public class Discount
    {
        public int Id { get; set; }
        public DiscountType Type { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public ICollection<Product> Products { get; set; } = [];
    }
}