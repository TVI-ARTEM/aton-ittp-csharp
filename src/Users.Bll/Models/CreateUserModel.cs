namespace Users.Bll.Models;

public record CreateUserModel(
    string Login,
    string Password,
    string Name,
    int Gender,
    DateTime? Birthday,
    bool Admin,
    DateTime CreatedOn,
    string CreatedBy
);