namespace Users.Api.Requests;

public record RestoreUserRequest(
    string Login,
    string Token
);