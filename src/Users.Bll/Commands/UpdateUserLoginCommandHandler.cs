using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record UpdateUserLoginCommand(
    string Login,
    string NewLogin,
    DateTime ModifiedOn,
    string Token
) : IRequest<UserInfoTokenModel>;

public class UpdateUserLoginCommandHandler : IRequestHandler<UpdateUserLoginCommand, UserInfoTokenModel>
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UpdateUserLoginCommandHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<UserInfoTokenModel> Handle(UpdateUserLoginCommand request,
        CancellationToken cancellationToken)
    {
        var userModifier = await _authService.AuthToken(request.Token, cancellationToken);

        if (request.Login == "admin" || (!userModifier.Admin && userModifier.Login != request.Login))
        {
            throw new ArgumentException("Access denied. Cannot change current user.");
        }

        var targetUser = await _userService.UpdateLoginUser(
            new UpdateUserLoginModel(
                Login: request.Login,
                NewLogin: request.NewLogin,
                ModifiedOn: request.ModifiedOn,
                ModifiedBy: request.Login == userModifier.Login ? request.NewLogin : userModifier.Login
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