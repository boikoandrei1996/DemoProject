using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DemoProject.DLL.Models
{
  public class AppRole : IdentityRole<Guid>
  {
    public const string Admin = "Admin";
    public const string Moderator = "Moderator";

    public static IReadOnlyList<string> GetAllRoles()
    {
      return new List<string>
      {
        Admin,
        Moderator
      };
    }
  }
}
