using System.ComponentModel.DataAnnotations.Schema;

namespace StarPingData.Models.Cart
{
    public class CartItemModel : BaseModel
    {
        public string? IdSessionOfCart { get; set; }
        public int? DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public DeviceModel? Device { get; set; }

        public int? SubscriptionId { get; set; }
        [ForeignKey("SubscriptionId")]
        public SubscriptionModel? Subscription { get; set; }
        public bool IsSubscription { get; set; }
        public int Quantity { get; set; }
    }
}
