using System.IO;

namespace MoviesAnalysisApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }

        public int DirectorId { get; set; }
        public Director Director { get; set; } = null!;

        public ICollection<MovieActor> MovieActors { get; set; } = null!;
        public ICollection<Genre> Genres { get; set; } = null!;
    }
}
