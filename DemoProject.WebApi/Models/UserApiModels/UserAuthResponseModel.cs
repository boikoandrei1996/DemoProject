using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public class UserAuthResponseModel
  {
    public UserViewModel User { get; set; }
    public string Token { get; set; }

    public static UserAuthResponseModel Map(AppUser model, string token)
    {
      return new UserAuthResponseModel
      {
        User = UserViewModel.Map(model),
        Token = token
      };
    }
  }
}
