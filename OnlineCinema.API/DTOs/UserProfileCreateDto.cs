namespace OnlineCinema.API.DTOs;

public class UserProfileCreateDto
{
    public int UserId { get; set; }
    public string? Avatar { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
