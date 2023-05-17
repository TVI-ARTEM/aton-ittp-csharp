using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Users.API.Controllers;

[ApiController]
[Route("/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task Create(CancellationToken token)
    {
        
    }

    [HttpPatch]
    public async Task Update(CancellationToken token)
    {
        
    }

    [HttpPatch]
    public async Task UpdatePassword(CancellationToken token)
    {
        
    }

    [HttpPatch]
    public async Task UpdateLogin(CancellationToken token)
    {
        
    }

    [HttpPatch]
    public async Task Restore(CancellationToken token)
    {
        
    }

    [HttpGet]
    public async Task GetActive(CancellationToken token)
    {
        
    }

    [HttpGet]
    public async Task GetLogin(CancellationToken token)
    {
        
    }

    [HttpGet]
    public async Task Get(CancellationToken token)
    {
        
    }
    
    [HttpGet]
    public async Task GetAge(CancellationToken token)
    {
        
    }
    
    [HttpDelete]
    public async Task Revoke(CancellationToken token)
    {
        
    }
}