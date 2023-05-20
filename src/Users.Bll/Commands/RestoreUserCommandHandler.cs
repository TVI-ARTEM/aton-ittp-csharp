using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record RestoreUserCommand(
    string Login,
    DateTime ModifiedOn,
    string Token
) : IRequest<UserInfoTokenModel>;

public class RestoreUserCommandHandler : IRequestHandler<RestoreUserCommand, UserInfoTokenModel>
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public RestoreUserCommandHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<UserInfoTokenModel> Handle(RestoreUserCommand request,
        CancellationToken cancellationToken)
    {
        var userModifier = await _authService.AuthToken(request.Token, cancellationToken);

        if (!userModifier.Admin && userModifier.Login != request.Login)
        {
            throw new AggregateException("Access denied. Cannot change current user.");
        }

        var targetUser = await _userService.RestoreUser(
            new RestoreUserModel(
                Login: request.Login,
                ModifiedOn: request.ModifiedOn,
                ModifiedBy: userModifier.Login
            ), cancellationToken
        );

        if (targetUser == null)
        {
            throw new ArgumentNullException(nameof(request.Login), "User is not found");
        }

        var token = await _authService.GenerateToken(new TokenParameters(
            Login: targetUser.Login,
            Password: targetUser.Password
        ), cancellationToken);

        return new UserInfoTokenModel(
            new UserInfoModel(
                Name: targetUser.Name,
                Gender: targetUser.Gender,
                Birthday: targetUser.Birthday,
                Revoked: targetUser.RevokedOn != null
            ),
            Token: token
        );
    }
}