using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineVerse.models;
using CineVerse.Data;
using CineVerse.DTOS;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Movies
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<MovieResponseDto>>> GetMovies()
    {
        var movies = await _context.movies.Include(m => m.Genres).ToListAsync();

        var res = movies.Select(m => new MovieResponseDto
        {
            Id = m.Id,
            Title = m.Title,
            ReleaseYear = m.ReleaseYear,
            PosterUrL = m.PosterUrL,
            VideoUrl = m.VideoUrl,
            Genres = m.Genres.Select(g => g.name).ToList()
        }).ToList();

        return Ok(res);
    }

    // GET: api/Movies/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<MovieResponseDto>> GetMovie(int id)
    {
        var m = await _context.movies.Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);

        if (m == null)
        {
            return NotFound(new { Message = "Movie not found" });
        }

        var res = new MovieResponseDto
        {
            Id = m.Id,
            Title = m.Title,
            ReleaseYear = m.ReleaseYear,
            PosterUrL = m.PosterUrL,
            VideoUrl = m.VideoUrl,
            Genres = m.Genres.Select(g => g.name).ToList()
        };

        return Ok(res);
    }

    // PUT: api/Movies/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, CreateMoviesDto mo)
    {
        var movie = await _context.movies.Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound(new { Message = "Movie not found" });
        }

        movie.Title = mo.Title;
        movie.ReleaseYear = mo.ReleaseYear;
        movie.PosterUrL = mo.PosterUrL;
        movie.VideoUrl = mo.VideoUrl;

        movie.Genres.Clear();
        if (mo.GenreIds != null && mo.GenreIds.Any())
        {
            var updatedGenres = await _context.Genres.Where(c => mo.GenreIds.Contains(c.id)).ToListAsync();
            movie.Genres = updatedGenres;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Movies
    [HttpPost]
    public async Task<ActionResult<MovieResponseDto>> PostMovie(CreateMoviesDto mo)
    {
        var movie = new Movie
        {
            Title = mo.Title,
            ReleaseYear = mo.ReleaseYear,
            PosterUrL = mo.PosterUrL,
            VideoUrl = mo.VideoUrl
        };

        if (mo.GenreIds != null && mo.GenreIds.Any())
        {
            var selectedGenres = await _context.Genres.Where(c => mo.GenreIds.Contains(c.id)).ToListAsync();
            movie.Genres = selectedGenres;
        }

        _context.movies.Add(movie);
        await _context.SaveChangesAsync();

        var res = new MovieResponseDto
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
            PosterUrL = movie.PosterUrL,
            VideoUrl = movie.VideoUrl,
            Genres = movie.Genres.Select(g => g.name).ToList()
        };

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, res);
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound(new { Message = "Movie not found" });
        }

        _context.movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieExists(int id)
    {
        return _context.movies.Any(e => e.Id == id);
    }
}