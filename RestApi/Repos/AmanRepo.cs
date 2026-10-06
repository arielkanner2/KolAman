using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<IEnumerable<IGrouping<string, Alert>>> GetByPriority()
    {
        return await _context.Alert.GroupBy(a => a.priority).ToListAsync();
    }
}