using Microsoft.EntityFrameworkCore;
using MovieAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Context
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                : base(options)
        {
        }

        public DbSet<Movies> Movies { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<MovieGenres> MovieGenres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenres>()
                .HasKey(bc => new { bc.MovieId, bc.GenreId });
            modelBuilder.Entity<MovieGenres>()
                .HasOne(bc => bc.Movie)
                .WithMany(b => b.MoviesGenres)
                .HasForeignKey(bc => bc.MovieId);
            modelBuilder.Entity<MovieGenres>()
                .HasOne(bc => bc.Genre)
                .WithMany(c => c.MoviesGenres)
                .HasForeignKey(bc => bc.GenreId);
        }
    }
}
