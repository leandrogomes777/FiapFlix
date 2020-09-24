using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Context;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieDetailsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MovieDetailsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/MovieDetails
        /// <summary>
        /// Retorna todos detalhes de filmes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDetail>>> GetMovieDetail()
        {
            return await _context.MovieDetail.ToListAsync();
        }

        // GET: api/MovieDetails/5
        /// <summary>
        /// Seleciona o detalhe de um filme pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDetail>> GetMovieDetail(long id)
        {
            var movieDetail = await _context.MovieDetail.FindAsync(id);

            if (movieDetail == null)
            {
                return NotFound();
            }

            return movieDetail;
        }

        // PUT: api/MovieDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Edita o detalhe do filme pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieDetail"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieDetail(long id, MovieDetail movieDetail)
        {
            if (id != movieDetail.MovieDetailId)
            {
                return BadRequest();
            }

            _context.Entry(movieDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieDetailExists(id))
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

        // POST: api/MovieDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Adiciona o detalhe de um filme
        /// </summary>
        /// <param name="movieDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MovieDetail>> PostMovieDetail(MovieDetail movieDetail)
        {
            _context.MovieDetail.Add(movieDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieDetail", new { id = movieDetail.MovieDetailId }, movieDetail);
        }

        // DELETE: api/MovieDetails/5
        /// <summary>
        /// Apaga o detalhe de um filme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDetail>> DeleteMovieDetail(long id)
        {
            var movieDetail = await _context.MovieDetail.FindAsync(id);
            if (movieDetail == null)
            {
                return NotFound();
            }

            _context.MovieDetail.Remove(movieDetail);
            await _context.SaveChangesAsync();

            return movieDetail;
        }

        private bool MovieDetailExists(long id)
        {
            return _context.MovieDetail.Any(e => e.MovieDetailId == id);
        }
    }
}
