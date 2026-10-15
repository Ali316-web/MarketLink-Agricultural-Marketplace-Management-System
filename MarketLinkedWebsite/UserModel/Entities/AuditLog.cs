using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalFarmersMarket.Entities
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? User { get; set; }

        public string Action { get; set; } = string.Empty;      // e.g., "CreateProduct", "DeleteOrder"
        public string EntityName { get; set; } = string.Empty;  // e.g., "Product"
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}