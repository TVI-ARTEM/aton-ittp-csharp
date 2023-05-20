namespace Users.Api.Responses;

public record UserTokenResponse(
    UserInfo UserInfo,
    string Token
);