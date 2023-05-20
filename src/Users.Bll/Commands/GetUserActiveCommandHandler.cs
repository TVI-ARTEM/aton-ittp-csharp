using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record GetUserActiveCommand(
    int Take,
    int Skip,
    string Token
) : IRequest<IEnumerable<UserInfoModel>>;

public class GetUserActiveCommandHandler : IRequestHandler<GetUserActiveCommand, IEnumerable<UserInfoModel>>
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public GetUserActiveCommandHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<IEnumerable<UserInfoModel>> Handle(GetUserActiveCommand request,
        CancellationToken cancellationToken)
    {
        await _authService.AuthAdminToken(request.Token, cancellationToken);

        var users = await _userService.GetUserByActive(
            new GetByActiveModel(
                Take: request.Take,
                Skip: request.Skip
            ), cancellationToken
        );

        return users.Select(
            it => new UserInfoModel(
                Name: it.Name,
                Gender: it.Gender,
                Birthday: it.Birthday,
                Revoked: it.RevokedOn != null
            )
        );

    }
}