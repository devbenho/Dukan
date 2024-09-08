using Application.Users;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Abstractions;
using Web.Contracts;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(ISender sender) : ApiController(sender)
{
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new CreateUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Username,
            request.Password,
            request.PhoneNumber,
            request.IsAdmin,
            request.IsSuperAdmin,
            request.IsActive,
            request.Addresses,
            request.Roles
        );

        var result = await Sender.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(result?.Error);
        }

        return Ok(result.Value);
    }
}