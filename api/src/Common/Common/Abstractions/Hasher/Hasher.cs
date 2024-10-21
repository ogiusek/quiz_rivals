using BCrypt.Net;
using Common.ValueObjects;

namespace Common.Abstractions.Hasher;

internal sealed class Hasher : IHasher
{
  public Hash Hash(string password) =>
    new(BCrypt.Net.BCrypt.HashPassword(password));


  public bool Verify(Hash passwordHash, string password) =>
    BCrypt.Net.BCrypt.Verify(password, passwordHash.Value);
}