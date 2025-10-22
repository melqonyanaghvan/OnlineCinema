using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Data;

namespace OnlineCinema.Infrastructure.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    Task<IEnumerable<Review>> GetReviewsByMovieAsync(int movieId);
}

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(CinemaDbContext context) : base(context) { }

    public async Task<IEnumerable<Review>> GetReviewsByMovieAsync(int movieId)
    {
        return await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .Include(r => r.User)
            .ToListAsync();
    }
}
