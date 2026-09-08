using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CineVerse.Data;
using CineVerse.models;

namespace CineVerse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchHistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SearchHistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMySearchHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Unauthorized access!" });
            }

            var history = await _context.searchHistories
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.SearchedAt)
                .Take(5)
                .Select(h => h.SearchTerm)
                .ToListAsync();

            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> AddSearchTerm([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest(new { Message = "Search term cannot be empty!" });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var historyEntry = new SearchHistory
            {
                SearchTerm = searchTerm.Trim(),
                UserId = userId!,
                SearchedAt = DateTime.UtcNow
            };

            _context.searchHistories.Add(historyEntry);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Search term saved successfully!" });
        }
    }
}