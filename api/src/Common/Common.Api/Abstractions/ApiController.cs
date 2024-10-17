using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Abstractions;

[Route("api/[controller]")]
public abstract class ApiController : CustomController
{
}