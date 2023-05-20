namespace Users.Api.Requests;

public record AuthRequest(
    string Login,
    string Password
);