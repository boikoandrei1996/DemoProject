using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.WebApi.Models.CartApiModels
{
  public class AddItemToCartModel
  {
    [Required]
    public Guid ShopItemDetailId { get; set; }

    public int? Count { get; set; }
  }
}
