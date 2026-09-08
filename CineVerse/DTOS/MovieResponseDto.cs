using Microsoft.EntityFrameworkCore.Storage.Json;
using System.ComponentModel.DataAnnotations;

namespace CineVerse.DTOS
{
    public class MovieResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string? PosterUrL { get; set; } = string.Empty;
        public string? VideoUrl { get; set; } = string.Empty;

        public List<string> Genres {  get; set; }=new List<string>();
    }
}
