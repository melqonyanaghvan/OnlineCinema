namespace OnlineCinema.API.DTOs;

public class MovieCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DurationMinutes { get; set; }
    public decimal Rating { get; set; }
}
