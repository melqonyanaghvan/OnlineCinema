using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Domain.Entities;
using OnlineCinema.Infrastructure.Repositories;
using OnlineCinema.API.DTOs;

namespace OnlineCinema.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActorsController : ControllerBase
{
    private readonly IActorRepository _actorRepository;

    public ActorsController(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Actor>>> GetAllActors()
    {
        var actors = await _actorRepository.GetAllAsync();
        return Ok(actors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Actor>> GetActor(int id)
    {
        var actor = await _actorRepository.GetByIdAsync(id);
        if (actor == null)
            return NotFound($"Actor with ID {id} not found.");
        return Ok(actor);
    }

    [HttpGet("with-movies")]
    public async Task<ActionResult<IEnumerable<Actor>>> GetActorsWithMovies()
    {
        var actors = await _actorRepository.GetActorsWithMoviesAsync();
        return Ok(actors);
    }

    [HttpPost]
    public async Task<ActionResult<Actor>> CreateActor([FromBody] ActorCreateDto actorDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var actor = new Actor
        {
            FirstName = actorDto.FirstName,
            LastName = actorDto.LastName,
            BirthDate = actorDto.BirthDate,
            Biography = actorDto.Biography
        };

        var createdActor = await _actorRepository.AddAsync(actor);
        return CreatedAtAction(nameof(GetActor), new { id = createdActor.Id }, createdActor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Actor>> UpdateActor(int id, [FromBody] ActorCreateDto actorDto)
    {
        var existingActor = await _actorRepository.GetByIdAsync(id);
        if (existingActor == null)
            return NotFound($"Actor with ID {id} not found.");

        existingActor.FirstName = actorDto.FirstName;
        existingActor.LastName = actorDto.LastName;
        existingActor.BirthDate = actorDto.BirthDate;
        existingActor.Biography = actorDto.Biography;

        var updatedActor = await _actorRepository.UpdateAsync(existingActor);
        return Ok(updatedActor);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActor(int id)
    {
        var result = await _actorRepository.DeleteAsync(id);
        if (!result)
            return NotFound($"Actor with ID {id} not found.");
        return NoContent();
    }
}
