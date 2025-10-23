using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Repositories;
using OnlineCinema.API.DTOs;

namespace OnlineCinema.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");
        return Ok(user);
    }

    [HttpGet("{id}/with-profile")]
    public async Task<ActionResult<User>> GetUserWithProfile(int id)
    {
        var user = await _userRepository.GetUserWithProfileAsync(id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] UserCreateDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            Username = userDto.Username,
            Email = userDto.Email,
            PasswordHash = userDto.PasswordHash,
            RegistrationDate = DateTime.UtcNow
        };

        var createdUser = await _userRepository.AddAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] UserCreateDto userDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
            return NotFound($"User with ID {id} not found.");

        existingUser.Username = userDto.Username;
        existingUser.Email = userDto.Email;
        existingUser.PasswordHash = userDto.PasswordHash;

        var updatedUser = await _userRepository.UpdateAsync(existingUser);
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var result = await _userRepository.DeleteAsync(id);
        if (!result)
            return NotFound($"User with ID {id} not found.");
        return NoContent();
    }
}
