namespace Common.Exceptions;

public sealed class ConfigurationException : Exception
{
  public ConfigurationException(string sectionName, string message) : base($"Configuration `{sectionName}`: {message}")
  {
  }
}