using System.Net.WebSockets;
using System.Text;
using Common.Types;

namespace Common.Extensions;

public static class WebSocketExtensions
{
  public static async Task OnMessage(this WebSocket webSocket, Func<WebSocketMessage, Task> onMessage, CancellationToken cancellationToken)
  {
    var buffer = new byte[1024];

    while (webSocket.State == WebSocketState.Open)
    {
      WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

      if (result.MessageType == WebSocketMessageType.Close)
      {
        if (webSocket.State == WebSocketState.Open)
        {
          await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        }
        return;
      }

      StringBuilder stringBuilder = new();
      stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));

      while (!result.EndOfMessage)
      {
        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
        if (result.MessageType == WebSocketMessageType.Close)
        {
          if (webSocket.State == WebSocketState.Open)
          {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
          }
          return;
        }

        stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
      }

      await onMessage(new WebSocketMessage(stringBuilder.ToString()));
    }
  }
}