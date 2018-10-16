using System;

namespace DemoProject.WebApi.Models.CartApiModels
{
  public class RemoveItemFromCartModel
  {
    public Guid CartId { get; set; }
    public Guid ShopItemDetailId { get; set; }
    public bool ShouldBeRemovedAllItems { get; set; }
  }
}
