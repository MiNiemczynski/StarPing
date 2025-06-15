using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StarPingData.Models
{
    public class OfferReviewModel : BaseReviewModel
    {
        public int OfferId { get; set; }
        [ForeignKey("OfferId")]
        [JsonIgnore]
        public OfferModel Offer { get; set; } = null!;
    }
}
