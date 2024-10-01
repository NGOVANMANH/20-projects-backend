using _1.Personal_Blogging_Platform_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace _1.Personal_Blogging_Platform_API.Data;

public class MongoDbContext : DbContext
{
    public MongoDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; init; }
}