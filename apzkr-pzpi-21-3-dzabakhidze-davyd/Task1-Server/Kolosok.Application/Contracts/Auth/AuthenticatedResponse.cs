namespace Kolosok.Application.Contracts.Auth;

public class AuthenticatedResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}