namespace Users.Api.Requests;

public record UpdateUserLoginRequest(
    string Login,
    string NewLogin,
    string Token
);