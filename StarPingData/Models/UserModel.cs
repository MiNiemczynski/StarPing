using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StarPingData.Models
{
    public class UserModel : BaseModel
    {
        [Required, MaxLength(255)]
        public string Name { get; set; } = null!;
        [Required, MaxLength(255)]
        public string Email { get; set; } = null!;
        [Required, MaxLength(255)]
        public string Password { get; set; } = null!;
        [MaxLength(25)]
        public string? PhoneNumber { get; set; }
        public bool IsBusiness { get; set; }
        [MaxLength(10)]
        public string? NIP { get; set; }
        [MaxLength(255)]
        public string? CompanyName { get; set; } = null!;

        public ICollection<AddressModel> Addresses { get; set; } = [];
        public ICollection<OrderModel> Orders { get; set; } = [];
        public ICollection<OfferReviewModel> OfferReviews { get; set; } = [];
        public ICollection<DeviceReviewModel> DeviceReviews { get; set; } = [];
    }
}
