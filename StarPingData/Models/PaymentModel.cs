using Newtonsoft.Json;
using StarPingData.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarPingData.Models
{
    public class PaymentModel : BaseModel
    {
        public decimal Amount { get; set; }
        [Required, MaxLength(255)]
        public PaymentStatusEnum Status { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public OrderModel Order { get; set; } = null!;

        public DateTime? PaymentDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
