using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.Users.Handlers;
using System.Reflection;

namespace CarHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;
    
    [HttpPost]
    [AllowAnonymous]
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var request = new Application.CQRS.Users.Handlers.UserRemove.UserDeleteCommand { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _sender.Send(new UserGetAll.GetAllUsersQuery());

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Data);
    }

    [HttpGet("GetById")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById([FromQuery] GetById.Query request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet("GetUserByEmail")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserByEmail([FromQuery] GetUserByEmail.GetUserByEmailCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet("AddFavoriteCar")]
    public async Task<IActionResult> AddUserFavorites([FromQuery] AddFavoriteCar.AddFavoriteCarCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet("GetUserFavorites")]
    public async Task<IActionResult> GetUserFavorites([FromQuery] GetUserFavorites.GetUserFavoritesCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpDelete("RemoveFavoriteCar")]
    public async Task<IActionResult> RemoveFavoriteCar([FromQuery] RemoveFavoriteCar.RemoveFavoriteCarCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] Application.CQRS.Users.Handlers.Login.LoginRequest request)
    {
        return Ok(await _sender.Send(request));
    }
}
