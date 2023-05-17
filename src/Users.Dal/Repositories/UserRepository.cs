
using Users.Bll.Repositories.Interfaces;
using Users.Dal.Contexts;

namespace Users.Dal.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }

   
}