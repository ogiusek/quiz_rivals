using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Abstractions;

[Authorize]
public abstract class CustomController : ControllerBase
{
}