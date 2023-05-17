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
}