using Microsoft.EntityFrameworkCore;

public class CommandsDbContext : DbContext
{
    public CommandsDbContext(DbContextOptions<CommandsDbContext> options)
        : base(options)
    {
    }
    public DbSet<Alert> Alert => Set<Alert>();
}

