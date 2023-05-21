using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record RevokeUserCommand(
    string Login,
    DateTime RevokedOn,
    string Token
) : IRequest<Unit>;

public class RevokeUserCommandHandler : IRequestHandler<RevokeUserCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public RevokeUserCommandHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<Unit> Handle(RevokeUserCommand request,
        CancellationToken cancellationToken)
    {
        var userModifier = await _authService.AuthAdminToken(request.Token, cancellationToken);

        if (request.Login == "admin")
        {
            throw new ArgumentException("Access denied. Cannot change current user.");
        }

        await _userService.RevokeUser(
            new RevokeUserModel(
                Login: request.Login,
                RevokedOn: request.RevokedOn,
                RevokedBy: userModifier.Login
            ), cancellationToken
        );

        return new Unit();
    }
}