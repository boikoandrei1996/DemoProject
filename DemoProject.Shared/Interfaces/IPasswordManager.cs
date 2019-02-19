namespace DemoProject.Shared.Interfaces
{
  public interface IPasswordManager
  {
    (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);
    bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
  }
}
