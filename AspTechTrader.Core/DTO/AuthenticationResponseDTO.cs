namespace AspTechTrader.Core.DTO
{
    public class AuthenticationResponseDTO
    {
        public string? PersonName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? AccessToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpirationTime { get; set; }

        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpirationDateTime { get; set; }
    }
}
