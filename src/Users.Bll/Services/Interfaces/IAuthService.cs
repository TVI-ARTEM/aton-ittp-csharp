using Users.Bll.Models;

namespace Users.Bll.Services.Interfaces;

public interface IAuthService
{
    Task<User> AuthToken(string token, CancellationToken cancellationToken);
    Task<User> AuthAdminToken(string token, CancellationToken cancellationToken);
    Task<string> GenerateToken(TokenParameters parameters, CancellationToken cancellationToken);
    Task<TokenParameters> ParseToken(string token, CancellationToken cancellationToken);
}