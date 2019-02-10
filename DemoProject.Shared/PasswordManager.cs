using System;
using System.Security.Cryptography;
using System.Text;
using DemoProject.Shared.Interfaces;

namespace DemoProject.Shared
{
  public sealed class PasswordManager : IPasswordManager
  {
    public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
    {
      if (string.IsNullOrWhiteSpace(password))
      {
        throw new ArgumentException("Value cannot be null, empty or whitespace.", nameof(password));
      }

      using (var hmac = new HMACSHA512())
      {
        var passwordSalt = hmac.Key;
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return (passwordHash, passwordSalt);
      }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      if (string.IsNullOrWhiteSpace(password))
      {
        throw new ArgumentException("Value cannot be null, empty or whitespace.", nameof(password));
      }

      if (passwordHash.Length != 64)
      {
        throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(passwordHash));
      }

      if (passwordSalt.Length != 128)
      {
        throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(passwordSalt));
      }

      using (var hmac = new HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        if (computedHash.Length != passwordHash.Length)
        {
          return false;
        }

        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i])
          {
            return false;
          }
        }
      }

      return true;
    }
  }
}
