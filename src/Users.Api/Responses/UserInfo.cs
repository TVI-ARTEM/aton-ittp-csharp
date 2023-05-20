namespace Users.Api.Responses;

public record UserInfo(
    string Name,
    int Gender,
    DateTime? Birthday,
    bool Revoked
);