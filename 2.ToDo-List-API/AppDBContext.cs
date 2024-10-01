using Microsoft.EntityFrameworkCore;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Todo> Todos { get; init; }
    public DbSet<User> Users { get; init; }
}