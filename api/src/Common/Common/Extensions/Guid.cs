using System.Numerics;
using System.Text;

namespace Common.Extensions;

public static class GuidExt
{
  public static string ToNumericString(this Guid guid, int length = 36) => new BigInteger(Encoding.UTF8.GetBytes(guid.ToString("N"))).ToString().Substring(0, length);
}