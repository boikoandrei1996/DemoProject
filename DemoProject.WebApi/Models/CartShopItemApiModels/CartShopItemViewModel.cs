using System;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.CartShopItemApiModels
{
  public class CartShopItemViewModel
  {
    public Guid ShopItemDetailId { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
    public string Title { get; set; }
    public string Kind { get; set; }
    public string ImagePath { get; set; }

    public static CartShopItemViewModel Map(CartShopItem model)
    {
      if (model == null)
      {
        return null;
      }

      return new CartShopItemViewModel
      {
        Count = model.Count,
        Price = model.Price,
        ShopItemDetailId = model.ShopItemDetailId,
        Kind = model.ShopItemDetail.Kind,
        Title = model.ShopItemDetail.ShopItem.Title,
        ImagePath = Constants.GetFullPathToImage(model.ShopItemDetail.ShopItem.ImagePath)
      };
    }
  }
}
