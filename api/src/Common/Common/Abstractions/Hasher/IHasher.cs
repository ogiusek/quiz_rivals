using Common.ValueObjects;

namespace Common.Abstractions.Hasher;

#nullable enable

public interface IHasher
{
  Hash Hash(string password);
  bool Verify(Hash passwordHash, string password);
}

#nullable restore