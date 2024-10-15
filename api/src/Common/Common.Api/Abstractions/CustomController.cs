using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Abstractions;

[Authorize]
[Route("api/[controller]")]
public abstract class CustomController : ControllerBase
{
  protected string ControllerPath() => $"{Request.Scheme}://{Request.Host}/api/{ControllerContext.ActionDescriptor.ControllerName}";
}