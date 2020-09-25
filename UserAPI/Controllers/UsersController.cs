using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        /// <summary>
        /// Retorna lista de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var result = _context.Users.Include(watched => watched.WatchedMovies).Include(later => later.WatchLaterMovies);
            return await result.ToListAsync();
        }

        // GET: api/Users/5
        /// <summary>
        /// Retorna usuário pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(long id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(long id, Users users)
        {
            if (id != users.UsersId)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        /// <summary>
        /// Marca o filme para ser assistido depois
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpPut("marktowatchlater/{userId}/{movieId}")]
        public async Task<IActionResult> MarkToWatchLater(long userId, long movieId)
        {
            var users = await _context.Users.FindAsync(userId);

            if (null == users)
            {
                return BadRequest();
            }

            if (users.WatchLaterMovies != null && users.WatchLaterMovies.FirstOrDefault(x => x.MovieId == movieId) != null)
            {
                return NoContent();
            }
            else
            {
                var markList = new List<WatchLaterMovies>
                {
                    new WatchLaterMovies() { Users = users, MovieId = movieId }
                };

                users.WatchLaterMovies = markList;
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(userId))
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


        /// <summary>
        /// Marca o filme como assistido pelo usuário
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpPut("markwatched/{userId}/{movieId}")]
        public async Task<IActionResult> Watched(long userId, long movieId)
        {
            var users = await _context.Users.FindAsync(userId);

            if (null == users)
            {
                return BadRequest();
            }

            if (users.WatchedMovies != null && users.WatchedMovies.FirstOrDefault(x => x.MovieId == movieId) != null)
            {
                return NoContent();
            }
            else
            {
                var markList = new List<WatchedMovies>
                {
                    new WatchedMovies() { Users = users, MovieId = movieId }
                };

                users.WatchedMovies = markList;
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(userId))
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

        /// <summary>
        /// Retorna os filmes ordernados pelos mais vistos
        /// </summary>
        /// <returns></returns>
        [HttpGet("getmostwatched/{lenght?}")]
        public async Task<ActionResult<Dictionary<long, long>>> MostWatched(int? lenght = 50)
        {
            try
            {
                var sublist = await _context.Users.SelectMany(x => x.WatchedMovies).ToListAsync();
                var data = sublist.GroupBy(x => x.MovieId).ToDictionary(c => c.Key, c => c.LongCount());
                data  = data.OrderByDescending(x=> x.Value).ToDictionary(x=> x.Key, x => x.Value);

                return data;            
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }


        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Inclui um usuário
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.UsersId }, users);
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Deleta um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(long id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(long id)
        {
            return _context.Users.Any(e => e.UsersId == id);
        }
    }
}
