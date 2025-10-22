namespace OnlineCinema.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DurationInMinutes { get; set; }
    public decimal Rating { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
}