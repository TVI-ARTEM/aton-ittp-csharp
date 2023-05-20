namespace Users.Bll.Models;

public record UpdateUserModel(
    string Login,
    string? Name,
    int? Gender,
    DateTime? Birthday,
    DateTime ModifiedOn,
    string ModifiedBy
);