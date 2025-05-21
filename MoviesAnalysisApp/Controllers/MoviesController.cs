using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesAnalysisApp.Data;
using MoviesAnalysisApp.Models;
using MoviesAnalysisApp.Services;


namespace MoviesAnalysisApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesAnalysisAppContext _context;
        private readonly TMDBService _tmdbService;

        public MoviesController(MoviesAnalysisAppContext context, TMDBService tmdbService)
        {
            _context = context;
            _tmdbService = tmdbService;
        }

        [HttpPost("import/{id}")]
        public async Task<IActionResult> ImportMovie(int id)
        {
            var rawJson = await _tmdbService.GetMovieRawAsync(id);

            // TODO: Deserialize, map, store in DB
            return Ok("Movie fetched. Mapping and saving needed.");
        }

        [HttpGet("top-directors")]
        public IActionResult TopDirectors()
        {
            var result = _context.Directors
                .Select(d => new { d.Name, Count = d.Movies.Count })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToList();
            return Ok(result);
        }

        [HttpGet("top-actors")]
        public IActionResult TopActors()
        {
            var result = _context.Actors
                .Select(a => new { a.Name, Count = a.MovieActors.Count })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToList();
            return Ok(result);
        }

        [HttpGet("genres-by-decade")]
        public IActionResult GenresByDecade()
        {
            var result = _context.Movies
                .GroupBy(m => m.ReleaseDate.Year / 10 * 10)
                .Select(g => new
                {
                    Decade = g.Key,
                    TopGenre = g.SelectMany(m => m.Genres)
                        .GroupBy(gen => gen.Name)
                        .OrderByDescending(genGroup => genGroup.Count())
                        .Select(genGroup => genGroup.Key)
                        .FirstOrDefault()
                })
                .ToList();

            return Ok(result);
        }
    }

}
