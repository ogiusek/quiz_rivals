using Common.Abstractions;
using Common.Abstractions.Hasher;
using Common.Adapters;
using Common.App.Abstractions;
using Common.Types;
using Common.ValueObjects;
using Users.Core.Models;

namespace Users.App.Commands.Register;

internal sealed class RegisterHandler : ICustomCommandHandler<RegisterCommand>
{
  bool ICustomCommandHandler<RegisterCommand>.RunAfterExecute { get; set; } = false;

  private readonly IHasher _hasher;
  private readonly IClock _clock;
  private readonly IRepository<User> _usersRepository;

  public RegisterHandler(IHasher hasher, IClock clock, IRepository<User> usersRepository)
  {
    _hasher = hasher;
    _clock = clock;
    _usersRepository = usersRepository;
  }


  async Task<Res> ICustomCommandHandler<RegisterCommand>.Execute(RegisterCommand command)
  {
    var user = new User(
      Id.New(),
      new(command.Nick),
      _clock.Now
    )
    {
      PasswordHash = _hasher.Hash(command.Password)
    };

    Res added = await _usersRepository.Add(user);
    return added;
  }
}