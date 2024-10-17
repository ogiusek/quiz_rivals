using Common.Types;

namespace Common.Tests.Types;

public class ResTests
{
  #region Arrange

  Res _res;
  Exception _exception;
  Exception _exception2;

  public ResTests()
  {
    _res = new Res();
    _exception = new Exception("Test exception");
    _exception2 = new Exception("Test exception 2");
  }

  #endregion

  [Fact]
  public void Is_Success_Should_Succed()
  {
    Assert.True(_res.Exceptions.Count() == 0, "empty res should have no exceptions");
    Assert.True(_res.IsSuccess, "empty res should be success");
    Assert.True(Res.Success().IsSuccess, "empty res should be success");
  }

  [Fact]
  public void Is_Failure_Should_Succed()
  {
    // Act
    _res.Exceptions = _res.Exceptions.Append(_exception);

    // Assert
    Assert.True(_res.Exceptions.Count() == 1, "res should have one exception");
    Assert.True(_res.Exceptions.FirstOrDefault() == _exception, "first exception should be the same");
    Assert.True(_res.IsFailure, "res should be failure");
    Assert.True(Res.Fail(_exception).IsFailure, "res should be failure");
    Assert.True(Res.Fail([_exception]).IsFailure, "res should be failure");
  }

  [Fact]
  public void Adding_Many_Exceptions_Should_Succed()
  {
    // Act
    _res.Exceptions = _res.Exceptions.Append(_exception).Append(_exception2);

    // Assert
    Assert.True(_res.Exceptions.Count() == 2, "res should have two exceptions");
    Assert.True(_res.Exceptions.FirstOrDefault() == _exception, "first exception should be the same");
    Assert.True(_res.Exceptions.LastOrDefault() == _exception2, "second exception should be the same");
    Assert.True(_res.IsFailure, "res should be failure");
    Assert.True(Res.Fail([_exception, _exception2]).IsFailure, "res should be failure");
  }
}