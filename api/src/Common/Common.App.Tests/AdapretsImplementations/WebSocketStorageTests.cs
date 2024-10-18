using System.Net.WebSockets;
using Common.Abstractions;
using Common.AbstractionsImplementations;
using Common.App.Adapters;
using Common.App.AdaptersImplementations;
using Common.Tests.AbstractionsMocks;
using Common.Types;
using Common.ValueObjects;

namespace Common.App.Tests.AdapretsImplementations;

public class WebSocketStorageTests
{
  #region Arrange
  IWebSocketsStorage webSocketsStorage;
  IAppWebSocket webSocket;

  public WebSocketStorageTests()
  {
    webSocketsStorage = new WebSocketsStorage();
    webSocket = new AppWebSocketMock(
      Id.New(),
      new Observer<WebSocketMessage>(),
      new Observer(),
      new Observer()
    );
  }
  #endregion

  [Fact]
  public async Task Add_And_Remove_WebSocket_Should_Succeed()
  {
    // Act
    Res addRes = await webSocketsStorage.Add(webSocket);
    IAppWebSocket? socket = await webSocketsStorage.Get(webSocket.Id);
    Res removeRes = await webSocketsStorage.Remove(webSocket.Id);
    IAppWebSocket? removedSocket = await webSocketsStorage.Get(webSocket.Id);

    // Assert
    Assert.True(webSocket == socket, "socket should equal to one added");
    Assert.True(addRes.IsSuccess, "adding not existing socket should succed");
    Assert.True(removeRes.IsSuccess, "removing existing socket should succed");
    Assert.True(removedSocket is null, "not existing socket should be null");
  }

  [Fact]
  public async Task Add_WebSocket_Should_Fail()
  {
    // Act
    Res resSuccess = await webSocketsStorage.Add(webSocket);
    Res resFail = await webSocketsStorage.Add(webSocket);
    IAppWebSocket? socket = await webSocketsStorage.Get(webSocket.Id);

    // Assert
    Assert.True(resSuccess.IsSuccess, "adding not existing socket should succed");
    Assert.True(resFail.IsFailure, "adding already existing socket should fail");
    int expectedCount = 1;
    Assert.True(expectedCount == resFail.Exceptions.Count(), "adding already existing socket should return one error");
    Assert.True(webSocket == socket, "socket should be equal to one added");
  }

  [Fact]
  public async Task Remove_WebSocket_Should_Fail()
  {
    // Act
    Res resFail = await webSocketsStorage.Remove(webSocket.Id);
    IAppWebSocket? socket = await webSocketsStorage.Get(webSocket.Id);

    // Assert
    Assert.True(resFail.IsFailure, "removing not existing socket should fail");
    Assert.True(socket is null, "not existing socket should be null");
  }
}