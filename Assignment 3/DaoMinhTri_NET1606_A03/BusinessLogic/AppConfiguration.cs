namespace BusinessLogic;

public class AppConfiguration
{
    public string JwtKey { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string AdminEmail { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
}