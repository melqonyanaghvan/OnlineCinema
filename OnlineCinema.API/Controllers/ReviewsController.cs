using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Repositories;
using OnlineCinema.API.DTOs;

namespace OnlineCinema.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewsController(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
    {
        var reviews = await _reviewRepository.GetAllAsync();
        return Ok(reviews);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetReview(int id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);
        
        if (review == null)
            return NotFound($"Review with ID {id} not found.");
        
        return Ok(review);
    }

    [HttpGet("movie/{movieId}")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByMovie(int movieId)
    {
        var reviews = await _reviewRepository.GetReviewsByMovieAsync(movieId);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<ActionResult<Review>> CreateReview([FromBody] ReviewCreateDto reviewDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var review = new Review
        {
            MovieId = reviewDto.MovieId,
            UserId = reviewDto.UserId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment,
            CreatedDate = DateTime.UtcNow
        };

        var createdReview = await _reviewRepository.AddAsync(review);
        return CreatedAtAction(nameof(GetReview), new { id = createdReview.Id }, createdReview);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Review>> UpdateReview(int id, [FromBody] ReviewCreateDto reviewDto)
    {
        var existingReview = await _reviewRepository.GetByIdAsync(id);
        if (existingReview == null)
            return NotFound($"Review with ID {id} not found.");

        existingReview.MovieId = reviewDto.MovieId;
        existingReview.UserId = reviewDto.UserId;
        existingReview.Rating = reviewDto.Rating;
        existingReview.Comment = reviewDto.Comment;

        var updatedReview = await _reviewRepository.UpdateAsync(existingReview);
        return Ok(updatedReview);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReview(int id)
    {
        var result = await _reviewRepository.DeleteAsync(id);
        
        if (!result)
            return NotFound($"Review with ID {id} not found.");
        
        return NoContent();
    }
}
