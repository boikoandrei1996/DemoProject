using System.Collections.Generic;

namespace DemoProject.DAL.Models
{
  public static class Role
  {
    public const string Admin = "Admin";
    public const string Moderator = "Moderator";

    public static IReadOnlyList<string> GetAllRoles()
    {
      return new List<string>
      {
        Role.Admin,
        Role.Moderator
      };
    }
  }
}
