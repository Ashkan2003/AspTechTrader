using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.DTO
{
    public class UserWatchListDeleteRequestDTO
    {
        [Required(ErrorMessage = "UserId cant be blank")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "UserWatchListId cant be blank")]
        public Guid UserWatchListId { get; set; }
    }
}
