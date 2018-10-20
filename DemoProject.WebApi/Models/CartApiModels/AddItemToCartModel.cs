using System;

namespace DemoProject.WebApi.Models.CartApiModels
{
  public class AddItemToCartModel
  {
    public Guid ShopItemDetailId { get; set; }
    public int? Count { get; set; }
  }
}
