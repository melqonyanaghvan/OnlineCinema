using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Data;

namespace OnlineCinema.Infrastructure.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserWithProfileAsync(int userId);
}

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CinemaDbContext context) : base(context) { }

    public async Task<User?> GetUserWithProfileAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.UserProfile)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}
