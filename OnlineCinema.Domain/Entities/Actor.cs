namespace OnlineCinema.Domain.Entities;

public class Actor
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; } = string.Empty;
    
    public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
}
