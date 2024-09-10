using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.DTO
{
    public class UserWatchListAddRequestDTO
    {

        [Required]
        public string userWatchListName { get; set; }

        [Required]
        public Guid UserId { get; set; }

    }
}
