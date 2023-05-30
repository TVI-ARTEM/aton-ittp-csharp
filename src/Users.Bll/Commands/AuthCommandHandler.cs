using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record AuthCommand(
    string Login,
    string Password
) : IRequest<UserInfoTokenModel>;

public class AuthCommandHandler : IRequestHandler<AuthCommand, UserInfoTokenModel>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthCommandHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<UserInfoTokenModel> Handle(AuthCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetUser(request.Login, cancellationToken);
        EnsureCorrectData(request, user);

        var token = await _authService.GenerateToken(new TokenParameters(
            user!.Login,
            user.Password
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

    private static void EnsureCorrectData(AuthCommand request, User? user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "Incorrect user's login!");

        if (user.Password.ToLower() != request.Password)
            throw new ArgumentException("Incorrect user's password!", nameof(request.Password));

        if (user.RevokedOn != null) throw new ArgumentException("User is revoked");
    }
}