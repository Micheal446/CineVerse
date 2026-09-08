using System;
using System.ComponentModel.DataAnnotations;

namespace CineVerse.models
{
    public class SearchHistory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string SearchTerm { get; set; } = string.Empty;

        public DateTime SearchedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}