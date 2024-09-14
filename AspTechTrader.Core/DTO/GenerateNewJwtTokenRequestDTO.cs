namespace AspTechTrader.Core.DTO
{
    public class GenerateNewJwtTokenRequestDTO
    {
        public string? Token { get; set; }
        public string? refreshToken { get; set; }
    }
}
