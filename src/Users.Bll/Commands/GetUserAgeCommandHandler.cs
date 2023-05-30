using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record GetUserAgeCommand(
    int Age,
    int Take,
    int Skip,
    string Token
) : IRequest<IEnumerable<UserInfoModel>>;

public class GetUserAgeCommandHandler : IRequestHandler<GetUserAgeCommand, IEnumerable<UserInfoModel>>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public GetUserAgeCommandHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<IEnumerable<UserInfoModel>> Handle(GetUserAgeCommand request,
        CancellationToken cancellationToken)
    {
        await _authService.AuthAdminToken(request.Token, cancellationToken);

        var users = await _userService.GetUserByAge(
            new GetByAgeModel(
                Age: request.Age,
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