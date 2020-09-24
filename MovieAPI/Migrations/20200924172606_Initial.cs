using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "MovieDetail",
                columns: table => new
                {
                    MovieDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieDescription = table.Column<string>(nullable: true),
                    PosterImageLink = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    MovieId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDetail", x => x.MovieDetailId);
                    table.ForeignKey(
                        name: "FK_MovieDetail_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieId = table.Column<long>(nullable: false),
                    GenreId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Genre" },
                values: new object[,]
                {
                    { 1L, "Adventure" },
                    { 20L, "(no genres listed)" },
                    { 19L, "Film-Noir" },
                    { 18L, "Western" },
                    { 17L, "IMAX" },
                    { 16L, "Documentary" },
                    { 14L, "War" },
                    { 13L, "Sci-Fi" },
                    { 12L, "Mystery" },
                    { 11L, "Horror" },
                    { 15L, "Musical" },
                    { 9L, "Crime" },
                    { 8L, "Action" },
                    { 7L, "Drama" },
                    { 6L, "Romance" },
                    { 5L, "Fantasy" },
                    { 4L, "Comedy" },
                    { 3L, "Children" },
                    { 2L, "Animation" },
                    { 10L, "Thriller" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Name", "ReleaseDate" },
                values: new object[,]
                {
                    { 8L, "Tom and Huck (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7L, "Sabrina (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6L, "Heat (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5L, "Father of the Bride Part II (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1L, "Toy Story (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, "Grumpier Old Men (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, "Jumanji (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9L, "Sudden Death (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, "Waiting to Exhale (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10L, "GoldenEye (1995)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "MovieId", "GenreId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 10L, 8L },
                    { 9L, 8L },
                    { 8L, 3L },
                    { 8L, 1L },
                    { 7L, 6L },
                    { 7L, 4L },
                    { 6L, 10L },
                    { 6L, 9L },
                    { 6L, 8L },
                    { 5L, 4L },
                    { 10L, 1L },
                    { 4L, 6L },
                    { 4L, 4L },
                    { 3L, 6L },
                    { 3L, 4L },
                    { 2L, 5L },
                    { 2L, 3L },
                    { 2L, 1L },
                    { 1L, 5L },
                    { 1L, 4L },
                    { 1L, 3L },
                    { 1L, 2L },
                    { 4L, 7L },
                    { 10L, 10L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieDetail_MovieId",
                table: "MovieDetail",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieDetail");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
