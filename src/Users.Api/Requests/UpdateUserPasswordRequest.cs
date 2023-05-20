namespace Users.Api.Requests;

public record UpdateUserPasswordRequest(
    string Login,
    string NewPassword,
    string Token
);