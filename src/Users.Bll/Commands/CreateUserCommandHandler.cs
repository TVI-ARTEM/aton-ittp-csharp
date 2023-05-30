using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record CreateUserCommand(
    string Login,
    string Password,
    string Name,
    int Gender,
    DateTime? Birthday,
    bool Admin,
    DateTime CreatedOn,
    string Token
) : IRequest<Unit>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<Unit> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var admin = await _authService.AuthAdminToken(request.Token, cancellationToken);

        await _userService.CreateUser(new CreateUserModel(
            Login: request.Login,
            Password: request.Password,
            Admin: request.Admin,
            Name: request.Name,
            Gender: request.Gender,
            Birthday: request.Birthday,
            CreatedOn: request.CreatedOn,
            CreatedBy: admin.Login
        ), cancellationToken);

        return new Unit();
    }
}