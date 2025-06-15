namespace StarPingData.Models.Cart
{
    public class CartDataModel
    {
        public List<CartItemModel> CartItems { get; set; } = [];
        public Dictionary<int, decimal> Total { get; set; } = [];
    }
}
