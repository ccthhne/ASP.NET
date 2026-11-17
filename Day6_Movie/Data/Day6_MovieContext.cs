using Microsoft.EntityFrameworkCore;

public class Day6_MovieContext(DbContextOptions<Day6_MovieContext> options) : DbContext(options)
{
    public DbSet<Day6_Movie.Models.Genre> Genre { get; set; } = default!;
    public DbSet<Day6_Movie.Models.Movie> Movie { get; set; } = default!;
}
