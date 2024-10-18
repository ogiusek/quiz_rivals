using Common.Abstractions;
using Common.AbstractionsImplementations;
using Common.Types;
using Common.ValueObjects;

namespace Common.Tests.AbstractionsImplementations;

public class ObserverTests
{
  #region Arrange

  Common.Abstractions.IObserver<string> _observer;
  IObserverListener<string> _listener => _observer;
  IObserverNotifier<string> _notifier => _observer;

  EventListener<string> _eventListener;
  string _expected = "abc";
  string? _notification;

  public ObserverTests()
  {
    _observer = new Observer<string>();
    _eventListener = new EventListener<string>(Id.New(), (notification) =>
    {
      _notification = notification;
      return Task.CompletedTask;
    });
  }

  #endregion Arrange

  [Fact]
  public async Task Lisner_Should_Be_Notified()
  {
    // Act
    Res resSuccess = _listener.Subscribe(_eventListener);
    await _notifier.Notify(_expected);

    // Assert
    Assert.True(resSuccess.IsSuccess, "subscribing should succed");
    Assert.True(_notification is not null, "notification shouldn't be null");
    Assert.True(_expected == _notification, "notification should be sent");
  }

  [Fact]
  public async void Unsubscribing_Should_Work()
  {
    // Act
    Res resSubscribe = _listener.Subscribe(_eventListener);
    Res resUnsubscribe = _listener.Unsubscribe(_eventListener.Id);
    await _notifier.Notify(_expected);

    // Assert
    Assert.True(resSubscribe.IsSuccess, "subscribing should succed");
    Assert.True(resUnsubscribe.IsSuccess, "unsubscribing should succed");
    Assert.True(_notification != _expected, "message shouldn't be handled by unsubscribed EventListener");
  }

  [Fact]
  public void Subscribing_Many_Times_Should_Fail()
  {
    // Act
    Res resSuccess = _listener.Subscribe(_eventListener);
    Res resFailure = _listener.Subscribe(_eventListener);

    // Assert
    Assert.True(resSuccess.IsSuccess, "subscribing once should success");
    Assert.True(resFailure.Exceptions.Count() == 1);
  }

  [Fact]
  public void Unsubscribing_Should_Fail()
  {
    // Act
    Res resFail = _listener.Unsubscribe(_eventListener.Id);

    // Assert
    Assert.True(resFail.IsFailure, "unsubscribing when not subscribed should fail");
    Assert.True(resFail.Exceptions.Count() == 1, "unsubscribing when not subscribed should throw one error");
  }
}