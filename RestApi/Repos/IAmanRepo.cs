public interface IAmanRepo
{
    Task<List<Alert>> GetAll();
    Task<IEnumerable<IGrouping<string, Alert>>> GetByPriority();
}