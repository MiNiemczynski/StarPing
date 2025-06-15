using System.ComponentModel.DataAnnotations;

namespace StarPingData.Models
{
    public abstract class BaseProductModel : BaseModel
    {
        [Required, MaxLength(255)]
        public string Name { get; set; } = null!;
        public int SpeedMbps { get; set; }
        public decimal Price { get; set; }
        [MaxLength(1200)]
        public string? Description { get; set; }
        public bool IsMobile { get; set; }

    }
}
