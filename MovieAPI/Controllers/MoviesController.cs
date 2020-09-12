using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Context;
using MovieAPI.Model;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MoviesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovies(long id)
        {
            var movies = await _context.Movies.FindAsync(id);

            if (movies == null)
            {
                return NotFound();
            }

            return movies;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovies(long id, Movies movies)
        {
            if (id != movies.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Movies>> PostMovies(Movies movies)
        {
            _context.Movies.Add(movies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovies", new { id = movies.MovieId }, movies);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movies>> DeleteMovies(long id)
        {
            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();

            return movies;
        }

        private bool MoviesExists(long id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
