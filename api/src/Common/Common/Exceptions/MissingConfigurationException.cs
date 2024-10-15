namespace Common.Exceptions;

public class MissingConfigurationException : Exception
{
  public MissingConfigurationException(string message) : base($"Missing configuration: {message}")
  {
  }
}