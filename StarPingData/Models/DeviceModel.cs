using System.ComponentModel.DataAnnotations;

namespace StarPingData.Models;
public class DeviceModel : BaseProductModel
{
    [MaxLength(255)]
    public string? ImageUrl { get; set; }
    public ICollection<DeviceReviewModel> Reviews { get; set; } = [];
    public ICollection<OrderDeviceModel> OrderDevices { get; set; } = [];
}