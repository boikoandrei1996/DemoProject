using System;
using System.Collections.Generic;

namespace DemoProject.DLL.Models
{
  public class ShopItem : BaseEntity
  {
    public ShopItem()
    {
      Details = new List<ShopItemDetail>();
      CartShopItems = new List<CartShopItem>();
    }

    public string Title { get; set; }
    
    public string Description { get; set; }

    public byte[] Image { get; set; }

    public string ImageContentType { get; set; }

    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }

    public ICollection<ShopItemDetail> Details { get; set; }
    public ICollection<CartShopItem> CartShopItems { get; set; }
  }
}
