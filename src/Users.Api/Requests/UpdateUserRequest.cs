namespace Users.Api.Requests;

public record UpdateUserRequest(
    string Login,
    string? Name,
    int? Gender,
    DateTime? Birthday,
    string Token
);