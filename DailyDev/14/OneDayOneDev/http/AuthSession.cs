public static class AuthSession
{
    public static string? AccessToken { get; private set; }
    public static DateTime? ExpireAtUtc { get; private set; }

    public static bool HasToken =>
        !string.IsNullOrWhiteSpace(AccessToken);

    public static bool IsAuthenticated =>
        HasToken &&
        ExpireAtUtc.HasValue &&
        DateTime.UtcNow < ExpireAtUtc.Value;

    public static bool IsExpired =>
        HasToken &&
        ExpireAtUtc.HasValue &&
        DateTime.UtcNow >= ExpireAtUtc.Value;

    public static void SetToken(string token, int expiresInSeconds)
    {
        AccessToken = token;
        ExpireAtUtc = DateTime.UtcNow.AddSeconds(expiresInSeconds);
    }

    public static void Clear()
    {
        AccessToken = null;
        ExpireAtUtc = null;
    }

    public static TimeSpan GetRemainingTime()
    {
        if (!ExpireAtUtc.HasValue)
            return TimeSpan.Zero;

        var remaining = ExpireAtUtc.Value - DateTime.UtcNow;
        return remaining < TimeSpan.Zero ? TimeSpan.Zero : remaining;
    }
}