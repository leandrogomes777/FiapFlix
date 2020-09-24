using Microsoft.EntityFrameworkCore;
using MovieAPI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MovieAPI.Models;

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
        public DbSet<MovieAPI.Models.MovieDetail> MovieDetail { get; set; }

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

            modelBuilder.Entity<Movies>()
                .HasOne(b => b.MovieDetail)
                .WithOne(c => c.Movies)
                .HasForeignKey<MovieDetail>(bc => bc.MovieId);


            Dictionary<string, long> _genresKeyValue = new Dictionary<string, long>();
            string[] genresFile = File.ReadAllLines("genres.csv");
            int _id = 0;

            foreach (string genreLine in genresFile)
            {
                _id++;
                modelBuilder.Entity<Genres>().HasData(
                    new Genres()
                    {
                        GenreId = _id,
                        Genre = genreLine
                    }
                    );

                _genresKeyValue.Add(genreLine, _id);
            }

            string[] moviesFile = File.ReadAllLines("movies.csv");
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            _id = 0;
            foreach (string movieLine in moviesFile)
            {
                string[] movieData = CSVParser.Split(movieLine);

                _id++;
                modelBuilder.Entity<Movies>().HasData(
                    new Movies()
                    {
                        MovieId = _id,
                        Name = movieData[1]
                    }
                    );

                string[] genres = movieData[2].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string genre in genres)
                {
                    modelBuilder.Entity<MovieGenres>().HasData(
                    new MovieGenres()
                    {
                        MovieId = _id,
                        GenreId = _genresKeyValue[genre]
                    }
                    );
                }
            }
        }        
    }
}
