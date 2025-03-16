using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.Users.Handlers;

namespace CarHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] Application.CQRS.Users.Handlers.Register.Command request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Application.CQRS.Users.Handlers.Update.Command request)
    {
        return Ok(await _sender.Send(request));
    }
}
