using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.EmailVertification.Handlers.EmailVerificationHandler;
using static Application.CQRS.EmailVertification.Handlers.VerifyEmailHandler;

namespace CarHub.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class GoogleAuthController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    [HttpPost("send-email-code")]
    public async Task<IActionResult> SendEmailCode([FromBody] GenerateEmailCodeCommand command)
    {
        await _sender.Send(command);
        return Ok("Kod göndərildi");
    }

    [HttpPost("verify-email-code")]
    public async Task<IActionResult> VerifyEmailCode([FromBody] VerifyEmailCodeCommand command)
    {
        var result = await _sender.Send(command);
        if (!result) return BadRequest("Kod yanlışdır və ya vaxtı bitib");

        return Ok("Email təsdiqləndi");
    }
}
