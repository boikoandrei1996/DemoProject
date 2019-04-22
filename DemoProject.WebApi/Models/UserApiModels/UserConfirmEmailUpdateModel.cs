using System.ComponentModel.DataAnnotations;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public sealed class UserConfirmEmailUpdateModel
  {
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
  }
}
