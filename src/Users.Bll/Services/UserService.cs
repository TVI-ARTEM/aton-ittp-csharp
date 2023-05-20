using Users.Bll.Models;
using Users.Bll.Repositories.Interfaces;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUser(string login, CancellationToken token)
    {
        return await _userRepository.Get(login: login, token: token);
    }

    public async Task<IEnumerable<User>> GetUserByAge(GetByAgeModel request, CancellationToken token)
    {
        return await _userRepository.GetByAge(request: request, token: token);
    }

    public async Task<IEnumerable<User>> GetUserByActive(GetByActiveModel request, CancellationToken token)
    {
        return await _userRepository.GetByActive(request: request, token: token);
    }

    public async Task CreateUser(CreateUserModel request, CancellationToken token)
    {
        await _userRepository.Create(request: request, token: token);
    }

    public async Task<User?> UpdateUser(UpdateUserModel request, CancellationToken token)
    {
        return await _userRepository.Update(request: request, token: token);
    }

    public async Task<User?> UpdateLoginUser(UpdateUserLoginModel request, CancellationToken token)
    {
        return await _userRepository.UpdateLogin(request: request, token: token);
    }

    public async Task<User?> UpdatePasswordUser(UpdateUserPasswordModel request, CancellationToken token)
    {
        return await _userRepository.UpdatePassword(request: request, token: token);
    }

    public async Task<User?> RestoreUser(RestoreUserModel request, CancellationToken token)
    {
        return await _userRepository.Restore(request: request, token: token);
    }

    public async Task RevokeUser(RevokeUserModel request, CancellationToken token)
    {
        await _userRepository.Revoke(request: request, token: token);
    }
}