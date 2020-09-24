using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using UserAPI.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }


    public DbSet<UserAPI.Models.Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        // ...
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>()
            .HasMany(bc => bc.WatchedMovies)
            .WithOne(c => c.Users)
            .HasForeignKey(d => d.UsersId);

        modelBuilder.Entity<Users>()
            .HasMany(bc => bc.WatchLaterMovies)
            .WithOne(c => c.Users)
            .HasForeignKey(d => d.UsersId);

        string[] namesFile = File.ReadAllLines("Nomes.csv");
        int _id = 0;
        Random rdQtt = new Random(12541);
        int iWatchedMoviesId = 0;
        int iWatchLaterMovies = 0;

        foreach (string nameLine in namesFile)
        {
            _id++;

            Users user = new Users();
            user.Name = nameLine;
            user.UsersId = _id;

            modelBuilder.Entity<Users>().HasData(
                    user
                );

            int moviesWatched = rdQtt.Next(1, 25);
            List<WatchedMovies> lstWatched = new List<WatchedMovies>();
            for (int i = 1; i <= moviesWatched; i++)
            {
                iWatchedMoviesId++;
                var watched = new WatchedMovies()
                {
                    WatchedMoviesId = iWatchedMoviesId,
                    UsersId = _id,
                    MovieId = rdQtt.Next(1, 9200),
                };

                lstWatched.Add(watched);
            }

            modelBuilder.Entity<WatchedMovies>().HasData(
                    lstWatched
                );

            int moviesWatchLater = rdQtt.Next(1, 10);
            List<WatchLaterMovies> lstWatchLaterMovies = new List<WatchLaterMovies>();
            for (int i = 1; i <= moviesWatchLater; i++)
            {
                iWatchLaterMovies++;
                var watchLater = new WatchLaterMovies()
                {
                    WatchLaterMoviesId = iWatchLaterMovies,
                    UsersId = _id,
                    MovieId = rdQtt.Next(1, 9200),
                };

                lstWatchLaterMovies.Add(watchLater);
            }
            modelBuilder.Entity<WatchLaterMovies>().HasData(
                    lstWatchLaterMovies
                );
        }
    }
}
