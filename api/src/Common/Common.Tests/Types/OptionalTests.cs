using Common.Types;

namespace Common.Tests.Types;

public class OptionalTests
{
  [Fact]
  public void Not_Specified_Should_Not_Have_Value()
  {
    var optional = new Optional<string>();
    Assert.False(optional.HasValue);
    Assert.Null(optional.Value);
  }

  [Fact]
  public void Specified_Should_Have_Value()
  {
    var optional = new Optional<string>("test");
    Assert.True(optional.HasValue);
    Assert.Equal("test", optional.Value);
  }

  [Fact]
  public void Not_Specified_Nullable_Should_Not_Have_Value()
  {
    var optional = new Optional<string?>();
    Assert.False(optional.HasValue);
    Assert.Null(optional.Value);
  }

  [Fact]
  public void Specified_Nullable_Should_Have_Value()
  {
    var optional = new Optional<string?>("test");
    Assert.True(optional.HasValue);
    Assert.Equal("test", optional.Value);
  }

  [Fact]
  public void Specified_Nullable_As_Null_Should_Have_Value()
  {
    var optional = new Optional<string?>(null);
    Assert.True(optional.HasValue);
    Assert.Null(optional.Value);
  }
}