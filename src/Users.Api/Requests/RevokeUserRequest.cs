namespace Users.Api.Requests;

public record RevokeUserRequest(
    string Login,
    string Token
);