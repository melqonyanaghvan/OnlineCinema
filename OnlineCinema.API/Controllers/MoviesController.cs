using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Repositories;
using OnlineCinema.API.DTOs;

namespace OnlineCinema.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
    {
        var movies = await _movieRepository.GetAllAsync();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null)
            return NotFound($"Movie with ID {id} not found.");
        return Ok(movie);
    }

    [HttpGet("with-actors")]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesWithActors()
    {
        var movies = await _movieRepository.GetMoviesWithActorsAsync();
        return Ok(movies);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> CreateMovie([FromBody] MovieCreateDto movieDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var movie = new Movie
        {
            Title = movieDto.Title,
            Description = movieDto.Description,
            ReleaseYear = movieDto.ReleaseYear,
            DurationMinutes = movieDto.DurationMinutes,
            Rating = movieDto.Rating
        };

        var createdMovie = await _movieRepository.AddAsync(movie);
        return CreatedAtAction(nameof(GetMovie), new { id = createdMovie.Id }, createdMovie);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Movie>> UpdateMovie(int id, [FromBody] MovieCreateDto movieDto)
    {
        var existingMovie = await _movieRepository.GetByIdAsync(id);
        if (existingMovie == null)
            return NotFound($"Movie with ID {id} not found.");

        existingMovie.Title = movieDto.Title;
        existingMovie.Description = movieDto.Description;
        existingMovie.ReleaseYear = movieDto.ReleaseYear;
        existingMovie.DurationMinutes = movieDto.DurationMinutes;
        existingMovie.Rating = movieDto.Rating;

        var updatedMovie = await _movieRepository.UpdateAsync(existingMovie);
        return Ok(updatedMovie);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMovie(int id)
    {
        var result = await _movieRepository.DeleteAsync(id);
        if (!result)
            return NotFound($"Movie with ID {id} not found.");
        return NoContent();
    }
}
