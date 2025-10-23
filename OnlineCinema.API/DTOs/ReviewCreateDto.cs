namespace OnlineCinema.API.DTOs;

public class ReviewCreateDto
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}
