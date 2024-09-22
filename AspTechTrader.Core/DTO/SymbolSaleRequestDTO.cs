using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.DTO
{
    public class SymbolSaleRequestDTO
    {
        [Required]
        public int SymbolSalePrice { get; set; }
        [Required]
        public int SymbolSaleQuantity { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid SymbolId { get; set; }
    }
}
