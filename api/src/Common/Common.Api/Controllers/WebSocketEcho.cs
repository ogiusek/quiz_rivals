using System.Net.WebSockets;
using Common.Api.Abstractions;
using Common.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Common.Api.Controllers;

[AllowAnonymous]
[Route("ws/[action]")]
public class WebSocketEchoControler : CustomController
{
  bool _isDevelopment;
  Common.Abstractions.IObserver<WebSocketMessage> _observer;

  public WebSocketEchoControler(IHostEnvironment hostEnvironment, Common.Abstractions.IObserver<WebSocketMessage> observer)
  {
    _isDevelopment = hostEnvironment.IsDevelopment();
    _observer = observer;
  }

  // [Route("echo")]
  public async Task<ActionResult> Echo()
  {
    var context = HttpContext;
    if (!_isDevelopment)
      return NotFound();

    if (!context.WebSockets.IsWebSocketRequest)
      return BadRequest("Request is not websocket connection request");

    WebSocket rawWebSocket = await context.WebSockets.AcceptWebSocketAsync();
    AppWebSocket webSocket = new AppWebSocket(Id.New(), rawWebSocket, _observer);

    EventListener<WebSocketMessage> echoEvent = new(Id.New(), webSocket.Send);
    webSocket.MessageListener.Subscribe(echoEvent);

    await webSocket.RunAsync();

    return new EmptyResult();
  }
}