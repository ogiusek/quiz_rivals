using System.Text;
using Common.Api.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace App;

[AllowAnonymous]
[Route("[action]")]
public class PingController : ApiController
{
  IEnumerable<EndpointDataSource> _endpoints;

  public PingController(IEnumerable<EndpointDataSource> endpoints)
  {
    _endpoints = endpoints;
  }

  [HttpGet]
  public ActionResult Ping() => Ok("Pong");

  [HttpGet]
  public ActionResult<string> Routes()
  {
    var sb = new StringBuilder();
    var endpoints = _endpoints.SelectMany(source => source.Endpoints);
    foreach (var endpoint in endpoints)
    {
      if (endpoint is RouteEndpoint routeEndpoint)
      {
        sb.AppendLine($"{routeEndpoint.RoutePattern.RawText} - {string.Join(", ", routeEndpoint.Metadata.OfType<HttpMethodMetadata>().SelectMany(m => m.HttpMethods))}");
      }
    }
    return Ok(sb.ToString());
  }
}

[Route("authorized")]
public class AuthorizedController : ApiController
{
  [HttpGet("ping")]
  public ActionResult Ping() => Ok("Pong is authorized");
}