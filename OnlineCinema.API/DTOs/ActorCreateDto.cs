namespace OnlineCinema.API.DTOs;

public class ActorCreateDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; } = string.Empty;
}
