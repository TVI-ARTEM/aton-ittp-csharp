using Users.Bll.Models;

namespace Users.Bll.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUser(string login, CancellationToken token);
    Task<IEnumerable<User>> GetUserByAge(GetByAgeModel request, CancellationToken token);
    Task<IEnumerable<User>> GetUserByActive(GetByActiveModel request, CancellationToken token);
    Task CreateUser(CreateUserModel request, CancellationToken token);
    Task<User?> UpdateUser(UpdateUserModel request, CancellationToken token);
    Task<User?> UpdateLoginUser(UpdateUserLoginModel request, CancellationToken token);
    Task<User?> UpdatePasswordUser(UpdateUserPasswordModel request, CancellationToken token);
    Task<User?> RestoreUser(RestoreUserModel request, CancellationToken token);
    Task RevokeUser(RevokeUserModel request, CancellationToken token);

}