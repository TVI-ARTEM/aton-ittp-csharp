namespace Users.Bll.Models;

public record UpdateUserLoginModel(
    string Login,
    string NewLogin,
    DateTime ModifiedOn,
    string ModifiedBy
);