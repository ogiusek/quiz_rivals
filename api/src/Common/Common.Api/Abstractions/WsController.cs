using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Abstractions;

[Route("ws/[controller]")]
public abstract class WsController : CustomController
{
}