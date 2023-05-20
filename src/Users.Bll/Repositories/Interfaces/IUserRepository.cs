using Users.Bll.Models;

namespace Users.Bll.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> Get(string login, CancellationToken token);
    Task<IEnumerable<User>> GetByAge(GetByAgeModel request, CancellationToken token);
    Task<IEnumerable<User>> GetByActive(GetByActiveModel request, CancellationToken token);
    Task Create(CreateUserModel request, CancellationToken token);
    Task<User?> Update(UpdateUserModel request, CancellationToken token);
    Task<User?> UpdateLogin(UpdateUserLoginModel request, CancellationToken token);
    Task<User?> UpdatePassword(UpdateUserPasswordModel request, CancellationToken token);
    Task<User?> Restore(RestoreUserModel request, CancellationToken token);
    Task Revoke(RevokeUserModel request, CancellationToken token);
}