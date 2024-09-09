namespace AspTechTrader.Core.Domain.Entities
{
    public class UserSymbolProperty
    {

        public Guid UserSymbolPropertyId { get; set; }
        public int SymbolPrice { get; set; }
        public int SymbolQuantity { get; set; }



        // relation to user
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        // relation to symbol
        public Guid? SymbolId { get; set; }
        public Symbol? Symbol { get; set; }

    }


}
