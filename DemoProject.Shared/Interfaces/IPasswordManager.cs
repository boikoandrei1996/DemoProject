namespace DemoProject.Shared.Interfaces
{
  public interface IPasswordManager
  {
    (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);
    bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
  }
}
