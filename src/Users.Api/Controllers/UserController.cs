using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Requests;
using Users.Api.Responses;
using Users.Bll.Commands;

namespace Users.Api.Controllers;

[ApiController]
[Route("/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreateUserResponse> Create(CreateUserRequest request, CancellationToken token)
    {
        await _mediator.Send(
            new CreateUserCommand(
                Login: request.Login,
                Password: request.Password,
                Name: request.Name,
                Gender: request.Gender,
                Birthday: request.Birthday,
                Admin: request.Admin,
                Token: request.Token,
                CreatedOn: DateTime.UtcNow
            ), token);

        return new CreateUserResponse();
    }

    [HttpPatch]
    public async Task<UserTokenResponse> Update(UpdateUserRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new UpdateUserCommand(
            Login: request.Login,
            Name: request.Name,
            Gender: request.Gender,
            Birthday: request.Birthday,
            ModifiedOn: DateTime.UtcNow,
            Token: request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                Name: result.UserInfo.Name,
                Gender: result.UserInfo.Gender,
                Birthday: result.UserInfo.Birthday,
                Revoked: result.UserInfo.Revoked
            ), result.Token);
    }

    [HttpPatch]
    public async Task<UserTokenResponse> UpdatePassword(UpdateUserPasswordRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new UpdateUserPasswordCommand(
            Login: request.Login,
            NewPassword: request.NewPassword,
            ModifiedOn: DateTime.UtcNow,
            Token: request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                Name: result.UserInfo.Name,
                Gender: result.UserInfo.Gender,
                Birthday: result.UserInfo.Birthday,
                Revoked: result.UserInfo.Revoked
            ), result.Token);
    }

    [HttpPatch]
    public async Task<UserTokenResponse> UpdateLogin(UpdateUserLoginRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new UpdateUserLoginCommand(
            Login: request.Login,
            NewLogin: request.NewLogin,
            ModifiedOn: DateTime.UtcNow,
            Token: request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                Name: result.UserInfo.Name,
                Gender: result.UserInfo.Gender,
                Birthday: result.UserInfo.Birthday,
                Revoked: result.UserInfo.Revoked
            ), result.Token);
    }

    [HttpPatch]
    public async Task<UserTokenResponse> Restore(RestoreUserRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new RestoreUserCommand(
            Login: request.Login,
            ModifiedOn: DateTime.UtcNow,
            Token: request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                Name: result.UserInfo.Name,
                Gender: result.UserInfo.Gender,
                Birthday: result.UserInfo.Birthday,
                Revoked: result.UserInfo.Revoked
            ), result.Token);
    }

    [HttpGet]
    public async Task<IEnumerable<UserInfo>> GetByActive([FromQuery] GetByActiveRequest request,
        CancellationToken token)
    {
        var result = await _mediator.Send(new GetUserActiveCommand(
            Take: request.Take,
            Skip: request.Skip,
            Token: request.Token
        ), token);

        return result.Select(it => new UserInfo(
            Name: it.Name,
            Gender: it.Gender,
            Birthday: it.Birthday,
            Revoked: it.Revoked
        ));
    }

    [HttpGet]
    public async Task<UserTokenResponse> GetByLogin([FromQuery] GetByLoginRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new GetUserLoginCommand(
            Login: request.Login,
            Token: request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                Name: result.UserInfo.Name,
                Gender: result.UserInfo.Gender,
                Birthday: result.UserInfo.Birthday,
                Revoked: result.UserInfo.Revoked
            ), result.Token);
    }


    [HttpGet]
    public async Task<IEnumerable<UserInfo>> GetByAge([FromQuery] GetByAgeRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new GetUserAgeCommand(
            Age: request.Age,
            Take: request.Take,
            Skip: request.Skip,
            Token: request.Token
        ), token);

        return result.Select(it => new UserInfo(
            Name: it.Name,
            Gender: it.Gender,
            Birthday: it.Birthday,
            Revoked: it.Revoked
        ));
    }

    [HttpGet]
    public async Task<UserTokenResponse> Auth([FromQuery] AuthRequest request,
        CancellationToken token)
    {
        var userInfo = await _mediator.Send(new AuthCommand(
            Login: request.Login,
            Password: request.Password
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                Name: userInfo.UserInfo.Name,
                Gender: userInfo.UserInfo.Gender,
                Birthday: userInfo.UserInfo.Birthday,
                Revoked: userInfo.UserInfo.Revoked
            ), userInfo.Token);
    }

    [HttpDelete]
    public async Task<RevokeUserResponse> Revoke([FromQuery] RevokeUserRequest request, CancellationToken token)
    {
        await _mediator.Send(new RevokeUserCommand(
            Login: request.Login,
            RevokedOn: DateTime.UtcNow,
            Token: request.Token
        ), token);
        return new RevokeUserResponse();
    }
}