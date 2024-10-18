namespace Common.Methods;

public static class StreamHelper
{
  public static bool StreamsMatch(Stream stream1, Stream stream2)
  {
    if (stream1.Length != stream2.Length)
      return false;

    (stream1.Position, stream2.Position) = (0, 0);
    for (int i = 0; i < stream1.Length; i++)
    {
      if (stream1.ReadByte() != stream2.ReadByte())
        return false;
    }
    return true;
  }
}