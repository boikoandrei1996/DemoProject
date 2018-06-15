using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.DLL.Models
{
  public class CartShopItem
  {
    public Guid CartId { get; set; }
    public Cart Cart { get; set; }

    public Guid ShopItemId { get; set; }
    public ShopItem ShopItem { get; set; }

    public int Count { get; set; }

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
  }
}
