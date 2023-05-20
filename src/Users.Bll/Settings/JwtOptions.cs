namespace Users.Bll.Settings;

public record JwtOptions
{
    public string Key { get; init; } = string.Empty;
}