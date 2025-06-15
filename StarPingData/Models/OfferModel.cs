namespace StarPingData.Models;
public class OfferModel : BaseProductModel
{
    public ICollection<OfferReviewModel> Reviews { get; set; } = [];
    public ICollection<SubscriptionModel> Subscriptions { get; set; } = [];
}

