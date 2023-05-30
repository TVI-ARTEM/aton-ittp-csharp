using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record GetUserLoginCommand(
    string Login,
    string Token
) : IRequest<UserInfoTokenModel>;

public class GetUserLoginCommandHandler : IRequestHandler<GetUserLoginCommand, UserInfoTokenModel>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public GetUserLoginCommandHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<UserInfoTokenModel> Handle(GetUserLoginCommand request,
        CancellationToken cancellationToken)
    {
        await _authService.AuthAdminToken(request.Token, cancellationToken);

        var user = await _userService.GetUser(
            request.Login, cancellationToken
        );

        if (user == null) throw new ArgumentNullException(nameof(request.Login), "User is not found");

        var token = await _authService.GenerateToken(new TokenParameters(
            Login: user.Login,
            Password: user.Password
        ), cancellationToken);

        return new UserInfoTokenModel(
            new UserInfoModel(
                Name: user.Name,
                Gender: user.Gender,
                Birthday: user.Birthday,
                Revoked: user.RevokedOn != null
            ),
            token
        );
    }
}