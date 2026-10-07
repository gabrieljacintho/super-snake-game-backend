namespace BertassoGamingServices.Core.DTOs
{
    public class AuthenticationResponse
    {
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public string? Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
