
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarPingData.Models
{
    public class OrderModel : BaseModel
    {
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public UserModel User { get; set; } = null!;

        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public AddressModel? Address { get; set; }

        public bool IsFinalized { get; set; }

        public ICollection<SubscriptionModel> Subscriptions { get; set; } = [];
        public ICollection<OrderDeviceModel> OrderDevices { get; set; } = [];
        public ICollection<PaymentModel> Payments { get; set; } = [];
    }
}
