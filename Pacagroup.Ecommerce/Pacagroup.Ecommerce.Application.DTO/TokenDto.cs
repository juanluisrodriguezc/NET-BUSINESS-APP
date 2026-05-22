namespace Pacagroup.Ecommerce.Application.DTO
{
    public sealed record TokenDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
    }
}
