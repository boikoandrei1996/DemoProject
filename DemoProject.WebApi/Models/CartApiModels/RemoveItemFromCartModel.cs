using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.WebApi.Models.CartApiModels
{
  public class RemoveItemFromCartModel
  {
    [Required]
    public Guid ShopItemDetailId { get; set; }

    public bool? ShouldBeRemovedAllItems { get; set; }
  }
}
