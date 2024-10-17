using System.Text.Json.Serialization;
using Common.Methods;

namespace Common.Tests.Methods;

internal record TestRecordWithoutPropertyName(string Id);
internal record TestRecordWithPropertyName([property: JsonPropertyName("test")] string Id);

public class JsonHelperTests
{
  [Fact]
  public void No_PropertyName_Should_ReturnPropertyName()
  {
    var result = JsonHelper.PropertyName<TestRecordWithoutPropertyName>("Id");
    Assert.Equal(nameof(TestRecordWithoutPropertyName.Id), result);
  }

  [Fact]
  public void With_PropertyName_Should_ReturnPropertyName()
  {
    var result = JsonHelper.PropertyName<TestRecordWithPropertyName>("Id");
    Assert.Equal("test", result);
  }
}