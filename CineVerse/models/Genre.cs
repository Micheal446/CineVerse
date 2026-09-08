using System.ComponentModel.DataAnnotations;

namespace CineVerse.models
{
    public class Genre
    {
        public int id { set; get;  }

        [Required]
        [MaxLength(50)]
        public string name { set; get; } = string.Empty;


        public ICollection<Movie> movies {  set; get; }=new List<Movie>();
    }
}
