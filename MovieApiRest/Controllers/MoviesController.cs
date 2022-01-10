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
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly WineDbContext _context;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(WineDbContext context, ILogger<MoviesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Wines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetWines()
        {
            _logger.LogDebug("D: Demande de la carte des vins");
            _logger.LogInformation("I: Demande de la carte des vins");
            return await _context.Wines.ToListAsync();
        }

        [HttpGet("allNoAsync")]
        public IEnumerable<Movie> GetWinesNoAsync()
        {
            // _logger.LogDebug("Demande de la carte des vins");
            // SQL : select * from Wines;
            return _context.Wines
                .ToList();
        }

        // GET: api/Wines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetWine(uint? id)
        {
            var wine = await _context.Wines.FindAsync(id);
            _logger.LogDebug($"Wine found #{id} : {wine}");
            if (wine == null)
            {
                return NotFound();
            }

            return wine;
        }

        // GET: api/Wines/5
        [HttpGet("asyncnoexcept/{id}")]
        public async Task<Movie> GetWineAsyncNoException(uint? id)
        {
            // null res is adapted as NoContent
            return await _context.Wines.FindAsync(id);
        }


        // GET: api/Wines/5
        [HttpGet("noasync/{id}")]
        public ActionResult<Movie> GetWineNoAsync(uint? id)
        {
            var wine =  _context.Wines.Find(id);

            if (wine == null)
            {
                return NotFound();
            }

            return wine;
        }

        [HttpGet("firstByAppellation")]
        public async Task<ActionResult<Movie>> GetFirstWineByAppellation(
            [FromBody] string appelation)
           // [FromQuery(Name = "a")]  string appelation)
        {
            _logger.LogDebug($"firstByAppellation: {appelation}");
            // SQL : select * from Wines w where w.Title = <appelation>
            try
            {
                return await _context.Wines.FirstAsync(w => w.Title == appelation);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            } 
        }

        [HttpGet("firstByAppellationOrSurprise")]
        public async Task<ActionResult<Movie>> GetFirstWineByAppellationOrSurprise(
            [FromBody] string appelation)
        {
            var defaultWine = new Movie { Title = "Nuit Saint Georges" };
            // SQL : select * from Wines w where w.Title = <appelation>
            var res =  await _context.Wines.FirstOrDefaultAsync(w => w.Title == appelation);
            return res ?? defaultWine;
        }

        // GET: api/Wines
        [HttpGet("AppellationColor")]
        public async Task<ActionResult<IEnumerable<AppellationColor>>> GetAppellattionColor()
        {
            return await _context.Wines
                    .Select(w => new AppellationColor
                    {
                        Appellation = w.Title,
                        Color = w.Color
                    }).ToListAsync();
        }

        // PUT: api/Wines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWine(uint? id, Movie wine)
        {
            if (id != wine.Id)
            {
                return BadRequest();
            }

            _context.Entry(wine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Wines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostWine(Movie wine)
        {
            _context.Wines.Add(wine);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetWine", new { id = wine.Id }, wine);
        }

        // DELETE: api/Wines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWine(uint? id)
        {
            var wine = await _context.Wines.FindAsync(id);
            if (wine == null)
            {
                return NotFound();
            }

            _context.Wines.Remove(wine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WineExists(uint? id)
        {
            // as boolean : select count(*) from Wines w where w.Id = id 
            return _context.Wines.Any(e => e.Id == id);
        }
    }
}
