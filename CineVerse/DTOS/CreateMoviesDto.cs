using CineVerse.models;
using System.ComponentModel.DataAnnotations;

namespace CineVerse.DTOS
{
    public class CreateMoviesDto
    {

        
        public int Id { get; set; }
        [Required(ErrorMessage = "enter title film ")]
        [MinLength(1)]
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;
        [Range(1888, 2026, ErrorMessage = "must shoud realy year bettwen 1888 and 2026")]
        public int ReleaseYear { get; set; }
        [Url(ErrorMessage = "enter url really correct")]
        [MaxLength(500)]

        public string PosterUrL { get; set; } = string.Empty;
        [Url(ErrorMessage = "enter url really correct")]

        [MaxLength(500)]
        public string VideoUrl { get; set; } = string.Empty;

        public ICollection<int> GenreIds { get; set; } = new List<int>();





    }
}
