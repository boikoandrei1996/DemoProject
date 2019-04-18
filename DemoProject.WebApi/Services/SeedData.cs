using System.Collections.Generic;
using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Services
{
  public static class SeedData
  {
    public static List<Cart> LoadCarts(IEnumerable<ShopItemDetail> shopItemDetails)
    {
      var carts = new List<Cart>();
      foreach (var shopItemDetail in shopItemDetails)
      {
        var cartShopItem = new CartShopItem
        {
          Count = 1,
          Price = shopItemDetail.Price,
          ShopItemDetailId = shopItemDetail.Id
        };
        carts.Add(new Cart
        {
          CartShopItems = new List<CartShopItem>() { cartShopItem }
        });
      }

      return carts;
    }

    public static List<Order> LoadOrders(IEnumerable<Cart> carts)
    {
      var orders = new List<Order>();
      var i = 0;
      foreach (var cart in carts)
      {
        i += 1;
        orders.Add(new Order
        {
          Name = $"Order #{i}",
          Mobile = "1234567",
          Address = $"Minsk pr.Pushkina {i}",
          CartId = cart.Id
        });
      }

      return orders;
    }
  }
}
