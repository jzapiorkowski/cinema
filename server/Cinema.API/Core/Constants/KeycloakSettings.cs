namespace Cinema.API.Core.Constants;

public class KeycloakSettings
{
    public string Authority { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public bool RequireHttpsMetadata { get; set; }
}