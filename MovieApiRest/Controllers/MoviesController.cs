using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieApiRest.Model;

namespace MovieApiRest.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(MovieDbContext context, ILogger<MoviesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            _logger.LogDebug("D: get all movies");
            _logger.LogInformation("I: get all movies");
            return await _context.Movies.ToListAsync();
        }

        
        // GET: api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int? id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _logger.LogDebug($"Movie found #{id} : {movie}");
            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpGet("byTitle")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByTitle(
                [FromQuery(Name = "t")]  string title)
        {
            _logger.LogDebug($"byTitle: {title}");
            return await _context.Movies
                .Where(m => m.Title.Contains(title))
                .ToListAsync();
        }
        

        // POST: api/movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        private bool MovieExists(int id)
        {
            // as boolean : select count(*) from Wines w where w.Id = id 
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
