namespace Users.Bll.Models;

public record UpdateUserPasswordModel(
    string Login,
    string NewPassword,
    DateTime ModifiedOn,
    string ModifiedBy
);