using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Data;

namespace OnlineCinema.Infrastructure.Repositories;

public interface IActorRepository : IRepository<Actor>
{
    Task<IEnumerable<Actor>> GetActorsWithMoviesAsync();
}

public class ActorRepository : Repository<Actor>, IActorRepository
{
    public ActorRepository(CinemaDbContext context) : base(context) { }

    public async Task<IEnumerable<Actor>> GetActorsWithMoviesAsync()
    {
        return await _context.Actors
            .Include(a => a.MovieActors)
            .ThenInclude(ma => ma.Movie)
            .ToListAsync();
    }
}
