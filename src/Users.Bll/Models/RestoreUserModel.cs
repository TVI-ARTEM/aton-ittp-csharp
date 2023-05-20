namespace Users.Bll.Models;

public record RestoreUserModel(
    string Login,
    DateTime ModifiedOn,
    string ModifiedBy
);