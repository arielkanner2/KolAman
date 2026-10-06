public interface IAmanRepo
{
    Task<List<Alert>> GetAll();
}