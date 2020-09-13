using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteAndClassificationService.Models;

namespace VoteAndClassificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGradesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MovieGradesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/MovieGrades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieGrade>>> GetMovieGrade()
        {
            return await _context.MovieGrade.ToListAsync();
        }

        // GET: api/MovieGrades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieGrade>> GetMovieGrade(long id)
        {
            var movieGrade = await _context.MovieGrade.FindAsync(id);

            if (movieGrade == null)
            {
                return NotFound();
            }

            return movieGrade;
        }

        // PUT: api/MovieGrades/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieGrade(long id, MovieGrade movieGrade)
        {
            if (id != movieGrade.MovieGradeID)
            {
                return BadRequest();
            }

            _context.Entry(movieGrade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieGradeExists(id))
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

        // POST: api/MovieGrades
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MovieGrade>> PostMovieGrade(MovieGrade movieGrade)
        {
            _context.MovieGrade.Add(movieGrade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieGrade", new { id = movieGrade.MovieGradeID }, movieGrade);
        }

        // DELETE: api/MovieGrades/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieGrade>> DeleteMovieGrade(long id)
        {
            var movieGrade = await _context.MovieGrade.FindAsync(id);
            if (movieGrade == null)
            {
                return NotFound();
            }

            _context.MovieGrade.Remove(movieGrade);
            await _context.SaveChangesAsync();

            return movieGrade;
        }

        private bool MovieGradeExists(long id)
        {
            return _context.MovieGrade.Any(e => e.MovieGradeID == id);
        }
    }
}
