using Microsoft.EntityFrameworkCore;

public class MongoDbContext : DbContext
{
    public MongoDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; init; }
    public DbSet<TaskData> TaskDatas { get; init; }
}