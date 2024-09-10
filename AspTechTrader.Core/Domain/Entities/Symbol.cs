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

        public int? Volume { get; set; }
        public int? LastDeal { get; set; }
        public float? LastDealPercentage { get; set; }
        public int? LastPrice { get; set; }
        public float? LastPricePercentage { get; set; }
        public int? TheFirst { get; set; }
        public int? TheLeast { get; set; }
        public int? TheMost { get; set; }
        public int? DemandVolume { get; set; }
        public int? DemandPrice { get; set; }
        public int? OfferPrice { get; set; }
        public int? OfferVolume { get; set; }
        public StateOptions? State { get; set; } = StateOptions.NOTALLOWED;
        public string? ChartNumber { get; set; }


        // relation
        //public List<User> Users { get; set; } = [];
        //public List<UserSymbol> UserSymbols { get; set; }

        // relation 
        public ICollection<UserSymbolProperty> UserSymbolProperties { get; set; }

        // relation
        public List<UserWatchList> UserWatchList { get; set; } = [];
    }
}
