namespace AspTechTrader.Core.DTO
{
    public class UserBoughtSymbolAddRequestDTO
    {
        public int SymbolPrice { get; set; }
        public int SymbolQuantity { get; set; }

        public Guid UserId { get; set; }
        public Guid SymbolId { get; set; }
    }
}
