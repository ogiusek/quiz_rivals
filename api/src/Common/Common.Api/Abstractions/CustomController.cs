using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Abstractions;

#nullable enable
[Authorize]
public abstract class CustomController : ControllerBase
{
  public string? UserClaim(string claim) => User.FindFirst(claim)?.Value;
}
#nullable restore