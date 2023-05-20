namespace Users.Bll.Models;

public record RevokeUserModel(
    string Login,
    DateTime RevokedOn,
    string RevokedBy
);