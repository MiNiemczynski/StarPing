using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarPingData.Models
{
    public class AddressModel : BaseModel
    {
        [Required, MaxLength(255)]
        public string Street { get; set; } = null!;
        [Required, MaxLength(255)]
        public string City { get; set; } = null!;
        [Required, MaxLength(255)]
        public string PostalCode { get; set; } = null!;
        [Required, MaxLength(255)]
        public string Region { get; set; } = null!;

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public UserModel? User { get; set; }

        [JsonIgnore]
        public ICollection<OrderModel> Orders { get; set; } = [];
    }
}
