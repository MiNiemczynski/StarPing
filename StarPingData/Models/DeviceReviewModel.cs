using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarPingData.Models
{
    public class DeviceReviewModel : BaseReviewModel
    {
        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        [JsonIgnore]
        public DeviceModel Device { get; set; } = null!;
    }
}
