using System.Reflection;
using System.Text.Json.Serialization;

namespace Common.Methods;

public static class JsonHelper
{
  public static string PropertyName<T>(string propertyName)
  {
    var propertyInfo = typeof(T).GetProperty(propertyName);
    if (propertyInfo == null)
    {
      return propertyName;
    }
    var attribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
    return attribute?.Name ?? propertyName;
  }
}