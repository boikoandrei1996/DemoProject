using System;
using System.Collections.Generic;
using DemoProject.DLL.Infrastructure;
using Newtonsoft.Json;

namespace DemoProject.DLL.Models
{
  public class ShopItem : BaseEntity
  {
    public string Title { get; set; }
    
    public string Description { get; set; }

    [JsonConverter(typeof(ImageJsonConverter))]
    public byte[] Image { get; set; }

    public string ImageContentType { get; set; }

    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }

    public ICollection<ShopItemDetail> Details { get; set; } = new List<ShopItemDetail>();
    public ICollection<CartShopItem> CartShopItems { get; set; } = new List<CartShopItem>();
  }
}
