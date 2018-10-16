using System;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.CartApiModels
{
  public class CartAddModel
  {
    public Guid Id { get; set; }

    public static Cart Map(CartAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new Cart { Id = model.Id };
    }
  }
}
