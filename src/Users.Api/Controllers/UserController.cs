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
                request.Login,
                request.Password,
                request.Name,
                request.Gender,
                request.Birthday,
                request.Admin,
                Token: request.Token,
                CreatedOn: DateTime.UtcNow
            ), token);

        return new CreateUserResponse();
    }

    [HttpPatch]
    public async Task<UserTokenResponse> Update(UpdateUserRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new UpdateUserCommand(
            request.Login,
            request.Name,
            request.Gender,
            request.Birthday,
            DateTime.UtcNow,
            request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                result.UserInfo.Name,
                result.UserInfo.Gender,
                result.UserInfo.Birthday,
                result.UserInfo.Revoked
            ), result.Token);
    }

    [HttpPatch]
    public async Task<UserTokenResponse> UpdatePassword(UpdateUserPasswordRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new UpdateUserPasswordCommand(
            request.Login,
            request.NewPassword,
            DateTime.UtcNow,
            request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                result.UserInfo.Name,
                result.UserInfo.Gender,
                result.UserInfo.Birthday,
                result.UserInfo.Revoked
            ), result.Token);
    }

    [HttpPatch]
    public async Task<UserTokenResponse> UpdateLogin(UpdateUserLoginRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new UpdateUserLoginCommand(
            request.Login,
            request.NewLogin,
            DateTime.UtcNow,
            request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                result.UserInfo.Name,
                result.UserInfo.Gender,
                result.UserInfo.Birthday,
                result.UserInfo.Revoked
            ), result.Token);
    }

    [HttpPatch]
    public async Task<UserTokenResponse> Restore(RestoreUserRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new RestoreUserCommand(
            request.Login,
            DateTime.UtcNow,
            request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                result.UserInfo.Name,
                result.UserInfo.Gender,
                result.UserInfo.Birthday,
                result.UserInfo.Revoked
            ), result.Token);
    }

    [HttpGet]
    public async Task<IEnumerable<UserInfo>> GetByActive([FromQuery] GetByActiveRequest request,
        CancellationToken token)
    {
        var result = await _mediator.Send(new GetUserActiveCommand(
            request.Take,
            request.Skip,
            request.Token
        ), token);

        return result.Select(it => new UserInfo(
            it.Name,
            it.Gender,
            it.Birthday,
            it.Revoked
        ));
    }

    [HttpGet]
    public async Task<UserTokenResponse> GetByLogin([FromQuery] GetByLoginRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new GetUserLoginCommand(
            request.Login,
            request.Token
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                result.UserInfo.Name,
                result.UserInfo.Gender,
                result.UserInfo.Birthday,
                result.UserInfo.Revoked
            ), result.Token);
    }


    [HttpGet]
    public async Task<IEnumerable<UserInfo>> GetByAge([FromQuery] GetByAgeRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new GetUserAgeCommand(
            request.Age,
            request.Take,
            request.Skip,
            request.Token
        ), token);

        return result.Select(it => new UserInfo(
            it.Name,
            it.Gender,
            it.Birthday,
            it.Revoked
        ));
    }

    [HttpGet]
    public async Task<UserTokenResponse> Auth([FromQuery] AuthRequest request,
        CancellationToken token)
    {
        var userInfo = await _mediator.Send(new AuthCommand(
            request.Login,
            request.Password
        ), token);

        return new UserTokenResponse(
            new UserInfo(
                userInfo.UserInfo.Name,
                userInfo.UserInfo.Gender,
                userInfo.UserInfo.Birthday,
                userInfo.UserInfo.Revoked
            ), userInfo.Token);
    }

    [HttpDelete]
    public async Task<RevokeUserResponse> Revoke([FromQuery] RevokeUserRequest request, CancellationToken token)
    {
        await _mediator.Send(new RevokeUserCommand(
            request.Login,
            DateTime.UtcNow,
            request.Token
        ), token);
        return new RevokeUserResponse();
    }
}