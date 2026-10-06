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
        return await _repository.GetAll();
    }
}