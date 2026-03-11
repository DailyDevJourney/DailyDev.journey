using OneDayOneDev.http;
using System.Net;
using System.Net.Http.Headers;

public class AuthHandler : DelegatingHandler
{
    public static event Action? SessionExpired;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {

        var path = request.RequestUri?.AbsolutePath?.ToLowerInvariant() ?? string.Empty;

        var isLoginRequest = path == "/api/auth/login";

        
        if (!isLoginRequest)
        {
            if (AuthSession.IsExpired)
            {
                AuthSession.Clear();
                SessionExpired?.Invoke();
                throw new UnauthorizedAccessException("Le token a expiré.");
            }

            if (!string.IsNullOrWhiteSpace(AuthSession.AccessToken))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", AuthSession.AccessToken);
            }
        }


        var response = await base.SendAsync(request, cancellationToken);

        
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            AuthSession.Clear();
            SessionExpired?.Invoke();
        }

        return response;
    }
}