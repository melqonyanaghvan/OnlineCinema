namespace OnlineCinema.Domain.Entities;

public class UserProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Avatar { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public User User { get; set; } = null!;
}
