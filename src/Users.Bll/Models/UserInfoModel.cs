namespace Users.Bll.Models;

public record UserInfoModel(
    string Name,
    int Gender,
    DateTime? Birthday,
    bool Revoked
);