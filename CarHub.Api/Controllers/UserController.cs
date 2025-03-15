using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] Application.CQRS.Users.Handlers.Register.Command request)
    {
        return Ok(await _sender.Send(request));
    }
}
