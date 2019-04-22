using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public sealed class UserAddModel
  {
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Username { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 5)]
    public string Role { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Password { get; set; }
    
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

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

    public static AppUser Map(UserAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new AppUser
      {
        Username = model.Username,
        Role = model.Role,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        PhoneNumber = model.PhoneNumber,
        LastModified = null,
        EmailConfirmed = false
      };
    }
  }
}
