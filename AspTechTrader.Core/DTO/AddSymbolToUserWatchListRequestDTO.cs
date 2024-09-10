using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.DTO
{
    public class AddSymbolToUserWatchListRequestDTO
    {
        [Required(ErrorMessage = "UserWatchListId cant be blank")]
        public Guid UserWatchListId { get; set; }

        [Required(ErrorMessage = "SymbolId cant be blank")]
        public Guid SymbolId { get; set; }
    }
}
