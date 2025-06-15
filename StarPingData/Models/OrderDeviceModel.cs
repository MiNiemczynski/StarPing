using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarPingData.Models
{
    public class OrderDeviceModel : BaseModel
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public OrderModel Order { get; set; } = null!;

        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public DeviceModel? Device { get; set; }
    }
}
