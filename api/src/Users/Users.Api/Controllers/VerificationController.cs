using Common.Api.Abstractions;
using Common.Api.Extensions;
using Common.App.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.App.Commands;
using Users.App.Commands.Login;
using Users.App.Commands.RegisterGuest;
using Users.App.Services.JwtStorage;

namespace Users.Api.Controllers;

[AllowAnonymous]
public class VerificationController(
  ICustomCommandHandler<RegisterGuestCommand> _registerGuestHandler,
  ICustomCommandHandler<RegisterCommand> _registerHandler,
  ICustomCommandHandler<LoginCommand> _loginHandler,
  IJwtGetter _jwtGetter
) : ApiController
{

  [HttpPost("register")]
  public async Task<ActionResult> Register([FromBody] RegisterCommand command)
  {
    var res = await _registerHandler.Handle(command);
    return res
      .WithSuccess(Ok)
      .WithCallback(_registerHandler.AfterHandle);
  }

  [HttpPost("register-guest")]
  public async Task<ActionResult> RegisterGuest([FromBody] RegisterGuestCommand command)
  {
    var res = await _registerGuestHandler.Handle(command);
    return res
      .WithSuccess(() => Ok(_jwtGetter.Get()))
      .WithCallback(_registerGuestHandler.AfterHandle);
  }

  [HttpPost("login")]
  public async Task<ActionResult> Login([FromBody] LoginCommand command)
  {
    var res = await _loginHandler.Handle(command);
    return res
      .WithSuccess(() => Ok(_jwtGetter.Get()))
      .WithCallback(_loginHandler.AfterHandle);
  }
}