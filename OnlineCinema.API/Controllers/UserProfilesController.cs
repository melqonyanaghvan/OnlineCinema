using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Repositories;
using OnlineCinema.API.DTOs;

namespace OnlineCinema.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfilesController : ControllerBase
{
    private readonly IUserProfileRepository _userProfileRepository;

    public UserProfilesController(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserProfile>>> GetAllUserProfiles()
    {
        var profiles = await _userProfileRepository.GetAllAsync();
        return Ok(profiles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
    {
        var profile = await _userProfileRepository.GetByIdAsync(id);
        
        if (profile == null)
            return NotFound($"UserProfile with ID {id} not found.");
        
        return Ok(profile);
    }

    [HttpPost]
    public async Task<ActionResult<UserProfile>> CreateUserProfile([FromBody] UserProfileCreateDto profileDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userProfile = new UserProfile
        {
            UserId = profileDto.UserId,
            Avatar = profileDto.Avatar,
            Phone = profileDto.Phone,
            DateOfBirth = profileDto.DateOfBirth
        };

        var createdProfile = await _userProfileRepository.AddAsync(userProfile);
        return CreatedAtAction(nameof(GetUserProfile), new { id = createdProfile.Id }, createdProfile);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserProfile>> UpdateUserProfile(int id, [FromBody] UserProfileCreateDto profileDto)
    {
        var existingProfile = await _userProfileRepository.GetByIdAsync(id);
        if (existingProfile == null)
            return NotFound($"UserProfile with ID {id} not found.");

        existingProfile.UserId = profileDto.UserId;
        existingProfile.Avatar = profileDto.Avatar;
        existingProfile.Phone = profileDto.Phone;
        existingProfile.DateOfBirth = profileDto.DateOfBirth;

        var updatedProfile = await _userProfileRepository.UpdateAsync(existingProfile);
        return Ok(updatedProfile);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserProfile(int id)
    {
        var result = await _userProfileRepository.DeleteAsync(id);
        
        if (!result)
            return NotFound($"UserProfile with ID {id} not found.");
        
        return NoContent();
    }
}
