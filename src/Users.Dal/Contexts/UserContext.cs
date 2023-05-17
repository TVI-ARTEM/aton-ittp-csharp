using Microsoft.EntityFrameworkCore;
using Users.Bll.Models;

namespace Users.Dal.Contexts;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
}