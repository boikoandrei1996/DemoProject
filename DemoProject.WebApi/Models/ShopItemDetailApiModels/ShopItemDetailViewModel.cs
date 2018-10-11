using System;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.ShopItemDetailApiModels
{
  public class ShopItemDetailViewModel
  {
    public Guid Id { get; set; }
    public int SubOrder { get; set; }
    public string Kind { get; set; }
    public string Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid ShopItemId { get; set; }

    public static ShopItemDetailViewModel Map(ShopItemDetail model)
    {
      if (model == null)
      {
        return null;
      }

      return new ShopItemDetailViewModel
      {
        Id = model.Id,
        SubOrder = model.SubOrder,
        Kind = model.Kind,
        Quantity = model.Quantity,
        Price = model.Price,
        ShopItemId = model.ShopItemId
      };
    }
  }
}
