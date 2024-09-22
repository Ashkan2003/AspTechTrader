using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.DTO
{
    public class UserPropertyUpdateRequestDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int UserProperty { get; set; }

    }
}
