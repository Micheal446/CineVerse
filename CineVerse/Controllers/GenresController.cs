using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineVerse.models;
using CineVerse.Data;
using CineVerse.DTOS;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[Authorize]
[ApiController]

public class GenresController : ControllerBase
{
    private readonly AppDbContext _context;

    public GenresController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Genres
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<GenreResponseDto>>> GetGenres()
    {
        var genres = await _context.Genres.Include(c => c.movies).ToListAsync();

        var res = genres.Select(g => new GenreResponseDto
        {
            Id = g.id,
            Name = g.name,
            moviescount = g.movies.Count
        }).ToList();

        return Ok(res);
    }

    // GET: api/Genres/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<GenreResponseDto>> GetGenre(int id)
    {
        var g = await _context.Genres.Include(c => c.movies).FirstOrDefaultAsync(c => c.id == id);

        if (g == null)
        {
            return NotFound(new { Message = "Genre not found" });
        }

        var res = new GenreResponseDto
        {
            Id = g.id,
            Name = g.name,
            moviescount = g.movies.Count
        };

        return Ok(res);
    }

    // PUT: api/Genres/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(int id, GenreResponseDto genre)
    {
        if (id != genre.Id)
        {
            return BadRequest(new { Message = "ID mismatch" });
        }

        var genres =await _context.Genres.FindAsync(id);
        if (genres==null)
        {

            return NotFound(new { Message = "Genre not found" });
        }
        genres.name = genre.Name;

        await _context.SaveChangesAsync();


        return NoContent();
    }

    // POST: api/Genres
    [HttpPost]
    public async Task<ActionResult<GenreResponseDto>> PostGenre(GenreResponseDto genre)
    {
        var genres = new Genre
        {
            name = genre.Name



        };

        _context.Genres.Add(genres);
        await _context.SaveChangesAsync();

        var res = new GenreResponseDto
        {
            Id = genres.id,
            Name = genres.name,
            moviescount = 0
        };

        return CreatedAtAction(nameof(GetGenre), new { id = genres.id }, res);
    }

    // DELETE: api/Genres/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound(new { Message = "Genre not found" });
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GenreExists(int id)
    {
        return _context.Genres.Any(e => e.id == id);
    }
}