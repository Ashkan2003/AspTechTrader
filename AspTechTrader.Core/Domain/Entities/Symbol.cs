using AspTechTrader.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.Domain.Entities
{
    public class Symbol
    {
        [Key]
        public Guid SymbolId { get; set; }

        [Required]
        public string SymbolName { get; set; }

        public int? volume { get; set; }
        public int? lastDeal { get; set; }
        public float? lastDealPercentage { get; set; }
        public int? lastPrice { get; set; }
        public float? lastPricePercentage { get; set; }
        public int? theFirst { get; set; }
        public int? theLeast { get; set; }
        public int? theMost { get; set; }
        public int? demandVolume { get; set; }
        public int? demandPrice { get; set; }
        public int? offerPrice { get; set; }
        public int? offerVolume { get; set; }
        public StateOptions? state { get; set; } = StateOptions.NOTALLOWED;
        public string? chartNumber { get; set; }
    }
}
