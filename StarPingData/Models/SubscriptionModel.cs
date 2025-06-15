using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarPingData.Models
{
    public class SubscriptionModel : BaseModel
    {
        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public OrderModel Order { get; set; } = null!;

        public int OfferId { get; set; }
        [ForeignKey("OfferId")]
        public OfferModel? Offer { get; set; }
        public int Months { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddMonths(Months);

    }
}
