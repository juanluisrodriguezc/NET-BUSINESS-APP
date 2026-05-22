namespace Pacagroup.Ecommerce.Application.DTO
{
    public sealed record SignInDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
