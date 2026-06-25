namespace TA.API.Infrastructure.Identity;

public class JwtOptions
{
    public string SecureKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int AccessTokenLifetimeInMinutes { get; set; }
    public int RefreshTokenLifetimeInDays { get; set; }
}