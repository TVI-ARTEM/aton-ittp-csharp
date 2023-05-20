namespace Users.Bll.Models;

public record UserInfoTokenModel(
    UserInfoModel UserInfo,
    string Token
);