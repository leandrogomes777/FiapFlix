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
    public class GenresController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public GenresController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        /// <summary>
        /// Retorna lista de gêneros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genres>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET: api/Genres/5
        /// <summary>
        /// Retorna gênero pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Genres>> GetGenres(long id)
        {
            var genres = await _context.Genres.FindAsync(id);

            if (genres == null)
            {
                return NotFound();
            }

            return genres;
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Atualiza gênero
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genres"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenres(long id, Genres genres)
        {
            if (id != genres.GenreId)
            {
                return BadRequest();
            }

            _context.Entry(genres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenresExists(id))
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

        // POST: api/Genres
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Cadastra gênero
        /// </summary>
        /// <param name="genres"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Genres>> PostGenres(Genres genres)
        {
            _context.Genres.Add(genres);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenres", new { id = genres.GenreId }, genres);
        }

        // DELETE: api/Genres/5
        /// <summary>
        /// Deleta gênero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genres>> DeleteGenres(long id)
        {
            var genres = await _context.Genres.FindAsync(id);
            if (genres == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genres);
            await _context.SaveChangesAsync();

            return genres;
        }

        private bool GenresExists(long id)
        {
            return _context.Genres.Any(e => e.GenreId == id);
        }
    }
}
