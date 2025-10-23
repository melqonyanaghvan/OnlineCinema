using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Data;

namespace OnlineCinema.Infrastructure.Repositories;

public interface IUserProfileRepository : IRepository<UserProfile>
{
}

public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(CinemaDbContext context) : base(context) { }
}
