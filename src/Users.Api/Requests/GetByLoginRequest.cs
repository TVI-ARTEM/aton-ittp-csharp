namespace Users.Api.Requests;

public record GetByLoginRequest(
    string Login,
    string Token
);