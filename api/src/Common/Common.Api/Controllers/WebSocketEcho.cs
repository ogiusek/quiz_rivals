using System.Net.WebSockets;
using Common.Api.Abstractions;
using Common.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Common.Api.Controllers;

[AllowAnonymous]
[Route("ws/[action]")]
public class WebSocketEchoControler : WsController
{
  bool _isDevelopment;
  Common.Abstractions.IObserver<WebSocketMessage> _messageObserver;
  Common.Abstractions.IObserver _openObserver;
  Common.Abstractions.IObserver _closeObserver;
  CancellationToken _cancellationToken;

  public WebSocketEchoControler(IHostEnvironment hostEnvironment, Common.Abstractions.IObserver<WebSocketMessage> messageObserver, Common.Abstractions.IObserver openObserver, Common.Abstractions.IObserver closeObserver, IHostApplicationLifetime lifetime)
  {
    _isDevelopment = hostEnvironment.IsDevelopment();
    _messageObserver = messageObserver;
    _openObserver = openObserver;
    _closeObserver = closeObserver;
    _cancellationToken = lifetime.ApplicationStopping;
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
    AppWebSocket webSocket = new AppWebSocket(Id.New(), rawWebSocket, _messageObserver, _closeObserver, _openObserver);

    EventListener<WebSocketMessage> echoEvent = new(Id.New(), webSocket.Send);
    webSocket.OnMessage.Subscribe(echoEvent);

    await webSocket.RunAsync(_cancellationToken);

    return new EmptyResult();
  }
}