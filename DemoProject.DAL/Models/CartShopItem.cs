using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.DAL.Models
{
  public sealed class CartShopItem
  {
    public Guid CartId { get; set; }

    public Cart Cart { get; set; }

    public Guid ShopItemDetailId { get; set; }

    public ShopItemDetail ShopItemDetail { get; set; }

    public int Count { get; set; }

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
  }
}
