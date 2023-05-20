namespace Users.Api.Responses;

public record GetUserLoginPasswordResponse(
    UserInfo UserInfo,
    string Token
);