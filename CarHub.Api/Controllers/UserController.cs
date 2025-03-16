using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.Users.Handlers;
using System.Reflection;

namespace CarHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] Application.CQRS.Users.Handlers.Register.RegisterCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Application.CQRS.Users.Handlers.Update.Command request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpDelete("Remove")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var request = new Application.CQRS.Users.Handlers.Remove.DeleteCommand { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _sender.Send(new GetAll.GetAllUsersQuery());

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Data);
    }
}
