using Microsoft.EntityFrameworkCore;

public class UrlShortenerDb : DbContext
{
    public UrlShortenerDb(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Url> Urls { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}