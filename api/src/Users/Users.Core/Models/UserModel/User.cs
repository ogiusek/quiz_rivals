using Common.Abstractions;
using Common.Types;
using Common.ValueObjects;
using FileSaver.Adapter.ValueObjects;
using Users.Core.Models.UserModel;
using Users.Core.Models.UserModel.Exceptions;

namespace Users.Core.Models;

public sealed class User
{
  private Id _id;
  public Id Id { get => _id; }

  private UserNick _nick;
  public UserNick Nick { get => _nick; init => _nick = value; }
  public User WithNick(UserNick nick) => new(this) { Nick = nick };

  public static IEnumerable<string> PhotoExtensions => new List<string> { "png", "jpg", "jpeg", "svg", "webp" };
  private FilePath _photoPath;
  /// <exception cref="InvalidPhotoExtension">allowed file extensions: [png, jpg, jpeg, svg, webp]</exception>
  public FilePath PhotoPath
  {
    get => _photoPath;
    init => _photoPath = !PhotoExtensions.Contains(value.Extension) ?
      throw new InvalidPhotoExtension(value.Extension) :
      value;
  }
  public User WithPhotoPath(FilePath photoPath) => new(this) { PhotoPath = photoPath };
  public static FilePath DefaultPhotoPath => new("user/default-icon.svg");

  private Email? _email;
  public Email? Email { get => _email; init => _email = value; }
  public User WithEmail(Email? email) => new(this) { Email = email };

  private Hash? _passwordHash;
  public Hash? PasswordHash { get => _passwordHash; init => _passwordHash = value; }
  public User WithPasswordHash(Hash? passwordHash) => new(this) { PasswordHash = passwordHash };

  private DateTime _createdAt;
  public DateTime CreatedAt { get => _createdAt; init => _createdAt = value; }

  private User(User dope)
  {
    _id = dope._id;
    _nick = dope._nick;
    _photoPath = dope._photoPath;
    _email = dope._email;
    _passwordHash = dope._passwordHash;
    _createdAt = dope._createdAt;
  }

  private User()
  {
    _id = Id.New();
    _nick = new UserNick("default");
    _photoPath = DefaultPhotoPath;
    _createdAt = DateTime.UtcNow;
  }

  public User(Id id, UserNick name, DateTime createdAt)
  {
    _id = id;
    _nick = name;
    _photoPath = DefaultPhotoPath;
    _createdAt = createdAt;
  }
}