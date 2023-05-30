using Microsoft.EntityFrameworkCore;
using Users.Bll.Models;
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

    public async Task<User?> Get(string login, CancellationToken token)
    {
        return await _context.Users.FirstOrDefaultAsync(it => it.Login == login, token);
    }

    public async Task<IEnumerable<User>> GetByAge(GetByAgeModel request, CancellationToken token)
    {
        var users = await _context.Users
            .Where(it =>
                it.Birthday.HasValue &&
                (DateTime.UtcNow - it.Birthday.Value).TotalDays / 365 > request.Age)
            .OrderBy(it => it.CreatedOn)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(token);

        return users;
    }

    public async Task<IEnumerable<User>> GetByActive(GetByActiveModel request, CancellationToken token)
    {
        return await _context.Users
            .Where(it => it.RevokedOn == null)
            .OrderBy(it => it.CreatedOn)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(token);
    }

    public async Task Create(CreateUserModel request, CancellationToken token)
    {
        await _context.Users.AddAsync(new User
        {
            Login = request.Login,
            Password = request.Password,
            Admin = request.Admin,
            Name = request.Name,
            Birthday = request.Birthday,
            Gender = request.Gender,
            Guid = Guid.NewGuid(),
            CreatedBy = request.CreatedBy,
            CreatedOn = request.CreatedOn
        }, token);

        await _context.SaveChangesAsync(token);
    }

    public async Task<User?> Update(UpdateUserModel request, CancellationToken token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(it => it.Login == request.Login, token);

        if (user == null) return user;

        if (request.Birthday != null) user.Birthday = request.Birthday;

        if (request.Name != null) user.Name = request.Name;

        if (request.Gender != null) user.Gender = (int)request.Gender;

        user.ModifiedOn = request.ModifiedOn;
        user.ModifiedBy = request.ModifiedBy;

        await _context.SaveChangesAsync(token);

        return user;
    }

    public async Task<User?> UpdateLogin(UpdateUserLoginModel request, CancellationToken token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(it => it.Login == request.Login, token);

        if (user == null) return user;

        user.Login = request.NewLogin;

        user.ModifiedOn = request.ModifiedOn;
        user.ModifiedBy = request.ModifiedBy;

        await _context.SaveChangesAsync(token);

        return user;
    }

    public async Task<User?> UpdatePassword(UpdateUserPasswordModel request, CancellationToken token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(it => it.Login == request.Login, token);

        if (user == null) return user;

        user.Password = request.NewPassword;

        user.ModifiedOn = request.ModifiedOn;
        user.ModifiedBy = request.ModifiedBy;

        await _context.SaveChangesAsync(token);
        return user;
    }

    public async Task<User?> Restore(RestoreUserModel request, CancellationToken token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(it => it.Login == request.Login, token);

        if (user == null) return user;

        user.RevokedOn = null;
        user.RevokedBy = null;

        user.ModifiedOn = request.ModifiedOn;
        user.ModifiedBy = request.ModifiedBy;

        await _context.SaveChangesAsync(token);
        return user;
    }

    public async Task Revoke(RevokeUserModel request, CancellationToken token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(it => it.Login == request.Login, token);

        if (user == null) return;

        user.RevokedOn = request.RevokedOn;
        user.RevokedBy = request.RevokedBy;


        await _context.SaveChangesAsync(token);
    }
}