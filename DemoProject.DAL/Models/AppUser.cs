using System;

namespace DemoProject.DAL.Models
{
  public class AppUser : BaseEntity
  {
    public string Username { get; set; }

    public string Role { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfCreation { get; set; }

    public DateTime? LastModified { get; set; }
  }
}
