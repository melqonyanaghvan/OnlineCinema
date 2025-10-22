using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Data;

namespace OnlineCinema.Infrastructure.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    Task<IEnumerable<Movie>> GetMoviesWithActorsAsync();
}

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(CinemaDbContext context) : base(context) { }

    public async Task<IEnumerable<Movie>> GetMoviesWithActorsAsync()
    {
        return await _context.Movies
            .Include(m => m.MovieActors)
            .ThenInclude(ma => ma.Actor)
            .ToListAsync();
    }
}
