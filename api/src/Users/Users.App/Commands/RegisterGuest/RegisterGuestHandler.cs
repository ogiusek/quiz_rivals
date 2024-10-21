using Common.Abstractions;
using Common.Adapters;
using Common.App.Abstractions;
using Common.Extensions;
using Common.Types;
using Common.ValueObjects;
using Users.App.Services.JwtGenerator;
using Users.App.Services.JwtStorage;
using Users.Core.Models;

namespace Users.App.Commands.RegisterGuest;

internal class RegisterGuestHandler :
  ICustomCommandHandler<RegisterGuestCommand>
{
  bool ICustomCommandHandler<RegisterGuestCommand>.RunAfterExecute { get; set; } = false;

  private readonly IJwtGenerator _jwtGenerator;
  private readonly IJwtSaver _jwtSaver;
  private readonly IClock _clock;
  private readonly IRepository<User> _usersRepository;

  public RegisterGuestHandler(IJwtGenerator jwtGenerator, IJwtSaver jwtSaver, IClock clock, IRepository<User> usersRepository)
  {
    _jwtGenerator = jwtGenerator;
    _jwtSaver = jwtSaver;
    _clock = clock;
    _usersRepository = usersRepository;
  }

  private static int UserNameLength => 64;
  private static string UserNamePrefix => "Guest";
  async Task<Res> ICustomCommandHandler<RegisterGuestCommand>.Execute(RegisterGuestCommand command)
  {
    string guidAsStringWithOnlyNumbers = Guid.NewGuid().ToNumericString(UserNameLength - UserNamePrefix.Length);
    var user = new User(
      Id.New(),
      new($"{UserNamePrefix}{guidAsStringWithOnlyNumbers}"),
      _clock.Now
    );

    Res added = await _usersRepository.Add(user);
    if (added.IsFailure)
    {
      return added;
    }

    var payload = new JwtPayload(user);
    var jwt = _jwtGenerator.Generate(payload);
    _jwtSaver.Save(jwt);

    return Res.Success();
  }
}