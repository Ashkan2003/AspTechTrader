using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.Domain.Entities
{
    public class UserWatchList
    {
        [Key]
        public Guid UserWatchListId { get; set; }

        [Required]
        public string userWatchListName { get; set; }



        // relation with User
        public Guid UserId { get; set; }

        public User User { get; set; }

        // relation with symbol 
        public List<Symbol> Symbols { get; set; } = [];

    }
}
