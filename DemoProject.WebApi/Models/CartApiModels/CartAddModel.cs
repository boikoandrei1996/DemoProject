using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.CartApiModels
{
  public class CartAddModel
  {
    public static Cart Map(CartAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new Cart();
    }
  }
}
