using Microsoft.EntityFrameworkCore;

public class AmanRepo : IAmanRepo
{
    private readonly CommandsDbContext _context;
    public AmanRepo(CommandsDbContext context)
    {
        _context = context;
    }
    public async Task<List<Alert>> GetAll()
    {
        return await _context.Alert.ToListAsync();
    }
}