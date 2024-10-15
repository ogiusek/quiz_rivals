using System.Net.WebSockets;
using Common.Types;

namespace Common.Extensions;

public static class WebSocketExtensions
{

  public static async Task OnMessage(this WebSocket webSocket, Func<WebSocketMessage, Task> onMessage, CancellationToken cancellationToken)
  {
    var buffer = new byte[1024];

    if (webSocket.State != WebSocketState.Open)
    {
      return;
    }

    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

    if (result.MessageType == WebSocketMessageType.Close)
    {
      if (webSocket.State == WebSocketState.Open)
      {
        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
      }
      return;
    }

    string text = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);

    while (!result.EndOfMessage)
    {
      result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
      if (result.MessageType == WebSocketMessageType.Close)
      {
        if (webSocket.State == WebSocketState.Open)
        {
          await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
        }
        return;
      }

      text += System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
    }

    await Task.WhenAll([
      onMessage(new WebSocketMessage(text)),
      OnMessage(webSocket, onMessage, cancellationToken)
    ]);
  }
}