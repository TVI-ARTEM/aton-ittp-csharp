using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record UpdateUserCommand(
    string Login,
    string? Name,
    int? Gender,
    DateTime? Birthday,
    DateTime ModifiedOn,
    string Token
) : IRequest<UserInfoTokenModel>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserInfoTokenModel>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public UpdateUserCommandHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<UserInfoTokenModel> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var userModifier = await _authService.AuthToken(request.Token, cancellationToken);

        if (request.Login == "admin" || (!userModifier.Admin && userModifier.Login != request.Login))
            throw new ArgumentException("Access denied. Cannot change current user.");

        var targetUser = await _userService.UpdateUser(
            new UpdateUserModel(
                Login: request.Login,
                Name: request.Name,
                Gender: request.Gender,
                Birthday: request.Birthday,
                ModifiedOn: request.ModifiedOn,
                ModifiedBy: userModifier.Login
            ), cancellationToken
        );

        if (targetUser == null) throw new ArgumentNullException(nameof(request.Login), "User is not found");

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
            token
        );
    }
}