using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarPingData.Models
{
    public abstract class BaseReviewModel : BaseModel
    {
        [MaxLength(1200)]
        public string? Comment { get; set; }
        public int Rate { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public UserModel? User { get; set; }

    }
}
