using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.DLL.Models
{
  public class ShopItemDetail : BaseEntity
  {
    public string Kind { get; set; }

    public string Quantity { get; set; }

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public Guid ShopItemId { get; set; }
    public ShopItem ShopItem { get; set; }
  }
}
