using Microsoft.EntityFrameworkCore;

public class CommandsDbContext : DbContext
{
    public CommandsDbContext(DbContextOptions<CommandsDbContext> options)
        : base(options)
    {
    }
    public DbSet<Alert> NORTHalerts => Set<Alert>();
    public DbSet<Alert> CENTERalerts => Set<Alert>();
    public DbSet<Alert> SOUTHalerts => Set<Alert>();
    public DbSet<Alert> OVERSEASalerts => Set<Alert>();
}

