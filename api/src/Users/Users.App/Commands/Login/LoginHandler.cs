using Common.Abstractions;
using Common.Abstractions.Hasher;
using Common.Adapters;
using Common.App.Abstractions;
using Common.Types;
using Microsoft.EntityFrameworkCore;
using Users.App.Commands.Login.Exceptions;
using Users.App.Services.JwtGenerator;
using Users.App.Services.JwtStorage;
using Users.Core.Models;
using Users.Core.Models.UserModel;

namespace Users.App.Commands.Login;

internal sealed class LoginHandler : ICustomCommandHandler<LoginCommand>
{
  bool ICustomCommandHandler<LoginCommand>.RunAfterExecute { get; set; } = false;

  private readonly IJwtGenerator _jwtGenerator;
  private readonly IJwtSaver _jwtSaver;
  private readonly IRepository<User> _usersRepository;
  private readonly IHasher _hasher;

  public LoginHandler(IJwtGenerator jwtGenerator, IJwtSaver jwtSaver, IRepository<User> usersRepository, IHasher hasher)
  {
    _jwtGenerator = jwtGenerator;
    _jwtSaver = jwtSaver;
    _usersRepository = usersRepository;
    _hasher = hasher;
  }

  async Task<Res> ICustomCommandHandler<LoginCommand>.Execute(LoginCommand command)
  {
    string nick = command.Nick;
    User user = await _usersRepository.Get.AsAsyncEnumerable().Where(u => u.Nick.Value == nick).SingleOrDefaultAsync();

    if (user is null)
    {
      return Res.Fail(new UserNotFound());
    }

    if (!_hasher.Verify(user.PasswordHash, command.Password))
    {
      return Res.Fail(new UserNotFound());
    }

    var payload = new JwtPayload(user);
    var jwt = _jwtGenerator.Generate(payload);
    _jwtSaver.Save(jwt);

    return Res.Success();
  }
}