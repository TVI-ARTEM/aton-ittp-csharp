namespace Users.Api.Requests;

public record CreateUserRequest(
    string Login,
    string Password,
    string Name,
    int Gender,
    DateTime? Birthday,
    bool Admin,
    string Token
);