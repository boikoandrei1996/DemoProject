using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public sealed class UserUpdateModel
  {
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(20)]
    [Phone]
    public string PhoneNumber { get; set; }

    public static AppUser Map(Guid id, UserUpdateModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new AppUser
      {
        Id = id,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        PhoneNumber = model.PhoneNumber
      };
    }
  }
}
