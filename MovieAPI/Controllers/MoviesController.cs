using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieAPI.Context;
using MovieAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        private HttpClient _clientVoteAndClassification;

        public MoviesController(DatabaseContext context, IConfiguration configurarion)
        {
            _context = context;
            _configuration = configurarion;

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            _clientVoteAndClassification = new HttpClient(clientHandler);

            _clientVoteAndClassification.BaseAddress = new Uri(_configuration["ClassificationAndVoteService"]);
        }

        // GET: api/Movies
        /// <summary>
        /// Retorna lista de filmes
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Atualiza os dados de um filme
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movies"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Cadastra um novo filme
        /// </summary>
        /// <param name="movies"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Movies>> PostMovies(Movies movies)
        {
            _context.Movies.Add(movies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovies", new { id = movies.MovieId }, movies);
        }

        // DELETE: api/Movies/5
        /// <summary>
        /// Deleta um filme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Procurar filmes por palavra
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [Route("[action]/{keyword}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> SearchMoviesByKeyWord(string keyword)
        {
            return await _context.Movies.Where(x => x.Name.Contains(keyword)).ToListAsync();
        }

        /// <summary>
        /// Retorna filmes ordenado pela nota
        /// </summary>
        /// <param name="genreID"></param>
        /// <returns></returns>
        [HttpGet("moviesbygrade/{maxLenght}")]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMoviesByGrade(long maxLenght)
        {
            HttpResponseMessage response = await _clientVoteAndClassification.
                                                                GetAsync($"getmoviesorderedbygrade/{maxLenght}");

            if (response.IsSuccessStatusCode)
            {
                List<long> classification = JsonConvert.DeserializeObject<List<long>>(response.Content.ReadAsStringAsync().Result);

                var allResults = await _context.Movies.ToListAsync();

                var result = from a in allResults.Select((r, i) => new { item = r, Index = i })
                              from b in classification.Select((r, i) => new { item = r, Index = i })
                                                .Where(b => a.item.MovieId == b.item).DefaultIfEmpty()
                              orderby (b == null ? allResults.Count() + a.Index : b.Index)
                              select a.item;

                return result.ToList();
            }
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Procura filme por gênero
        /// </summary>
        /// <param name="genreID"></param>
        /// <returns></returns>
        [Route("[action]/{genreID}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> SearchMoviesByGenre(long genreID)
        {
            var resultGenres = _context.MovieGenres.Where(x => x.GenreId == genreID).Select(x => x.MovieId);

            return await _context.Movies.Where(x => resultGenres.Contains(x.MovieId)).ToListAsync();
        }

        /// <summary>
        /// Retorna filmes pelo gênero ordenado pelos mais assistidos
        /// </summary>
        /// <param name="maxLenght"></param>
        /// <returns></returns>
        [HttpGet("moviesbygenreandmostwatched/{maxLenght}")]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMoviesByGenresAndMostWatched (long maxLenght)
        {
            HttpResponseMessage response = await _clientVoteAndClassification.
                                                                GetAsync($"getmoviesorderedbygrade/{maxLenght}");

            if (response.IsSuccessStatusCode)
            {
                List<long> classification = JsonConvert.DeserializeObject<List<long>>(response.Content.ReadAsStringAsync().Result);

                var allResults = await _context.Movies.ToListAsync();

                var result = from a in allResults.Select((r, i) => new { item = r, Index = i })
                             from b in classification.Select((r, i) => new { item = r, Index = i })
                                               .Where(b => a.item.MovieId == b.item).DefaultIfEmpty()
                             orderby (b == null ? allResults.Count() + a.Index : b.Index)
                             select a.item;

                return result.ToList();
            }
            else
            {
                return BadRequest(response);
            }
        }

    }
}
