using Common.Api.Abstractions;
using Common.Api.Extensions;
using Common.App.Abstractions;
using FileSaver.Adapter.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.App.Abstractions;
using Users.App.Commands;
using Users.App.Commands.SetEmail;
using Users.App.Commands.SetNick;
using Users.App.Commands.SetPassword;
using Users.App.Commands.SetPhoto;
using Users.App.Queries.Profile;

namespace Users.Api.Controllers;

public class UserController(
  ICustomQueryHandler<ProfileQuery, ProfileQueryResponse> profileHandler,
  ICustomCommandHandler<SetEmailCommand> setEmailHandler,
  ICustomCommandHandler<SetNickCommand> _setNickHandler,
  ICustomCommandHandler<SetPasswordCommand> _setPasswordHandler,
  ICustomCommandHandler<SetPhotoCommand> _setPhotoHandler
) : ApiController
{
  [HttpGet("profile")]
  public async Task<ActionResult<ProfileQueryResponse>> Profile()
  {
    var query = new ProfileQuery(UserClaim(SessionClaims.Id));
    var res = await profileHandler.Handle(query);
    return res
      .WithSuccess(() => Ok(res.Value));
  }

  [HttpPost("set-email")]
  public async Task<ActionResult> SetEmail([FromBody] SetEmailCommand command)
  {
    command = command with { Id = UserClaim(SessionClaims.Id) };
    var res = await setEmailHandler.Handle(command);
    return res
      .WithSuccess(Ok)
      .WithCallback(setEmailHandler.AfterHandle);
  }

  [HttpPost("set-nick")]
  public async Task<ActionResult> SetNick([FromBody] SetNickCommand command)
  {
    command = command with { Id = UserClaim(SessionClaims.Id) };
    var res = await _setNickHandler.Handle(command);
    return res
      .WithSuccess(Ok)
      .WithCallback(_setNickHandler.AfterHandle);
  }

  [HttpPost("set-password")]
  public async Task<ActionResult> SetPassword([FromBody] SetPasswordCommand command)
  {
    command = command with { Id = UserClaim(SessionClaims.Id) };
    var res = await _setPasswordHandler.Handle(command);
    return res
      .WithSuccess(Ok)
      .WithCallback(_setPasswordHandler.AfterHandle);
  }

  [HttpPost("set-photo")]
  public async Task<ActionResult> SetPhoto([FromForm] IFormFile file)
  {
    SetPhotoCommand command = new(
      UserClaim(SessionClaims.Id),
      new(
        file.OpenReadStream(),
        FileExtension.GetExtension(new FileContentType(file.ContentType))
      )
    );
    var res = await _setPhotoHandler.Handle(command);
    return res
      .WithSuccess(Ok)
      .WithCallback(_setPhotoHandler.AfterHandle);
  }
}