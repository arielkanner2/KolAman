using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AmanController : ControllerBase
{
    private readonly IAmanRepo _repository;
    public AmanController(IAmanRepo repo)
    {
        _repository = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Alert>>> GetAll()
    {
        // System.Console.WriteLine(await _repository.GetByPriority());
        return Ok(await _repository.GetAll());
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IGrouping<string, Alert>>>> GetByPriority()
    {
        return Ok(await _repository.GetAll());
    }
    
}