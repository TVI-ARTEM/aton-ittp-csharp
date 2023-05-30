using System.Text;
using System.Text.Json;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
using Users.Bll.Models;
using Users.Bll.Repositories.Interfaces;
using Users.Bll.Services.Interfaces;
using Users.Bll.Settings;

namespace Users.Bll.Services;

public class AuthService : IAuthService
{
    private readonly JwtOptions _options;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IOptions<JwtOptions> options)
    {
        _userRepository = userRepository;
        _options = options.Value;
    }

    public async Task<User> AuthToken(string token, CancellationToken cancellationToken)
    {
        var parameters = await ParseToken(token, cancellationToken);
        var user = await _userRepository.Get(parameters.Login, cancellationToken);

        if (user == null) throw new ArgumentNullException(nameof(token), "Incorrect token. User not found.");

        if (user.Password.ToLower() != parameters.Password.ToLower())
            throw new ArgumentException("Incorrect token. Incorrect password.");

        if (user.RevokedOn != null) throw new ArgumentException("User is revoked");

        return user;
    }

    public async Task<User> AuthAdminToken(string token, CancellationToken cancellationToken)
    {
        var user = await AuthToken(token, cancellationToken);

        if (!user.Admin) throw new ArgumentException("Access denied. User is not admin.");

        return user;
    }


    public Task<string> GenerateToken(TokenParameters parameters, CancellationToken cancellationToken)
    {
        return Task.FromResult(new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.ASCII.GetBytes(_options.Key))
            .AddClaim("exp", DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds())
            .AddClaim("login", parameters.Login)
            .AddClaim("password", parameters.Password)
            .Encode());
    }

    public Task<TokenParameters> ParseToken(string token, CancellationToken cancellationToken)
    {
        var dict = new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.ASCII.GetBytes(_options.Key))
            .MustVerifySignature()
            .Decode<IDictionary<string, object>>(token);

        var tokenParams = new TokenParameters(
            ((JsonElement)dict["login"]).GetString() ?? "",
            ((JsonElement)dict["password"]).GetString() ?? ""
        );
        return Task.FromResult(tokenParams);
    }
}