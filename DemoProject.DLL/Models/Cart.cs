using System.Collections.Generic;

namespace DemoProject.DLL.Models
{
  public class Cart : BaseEntity
  {
    public ICollection<CartShopItem> CartShopItems { get; set; } = new List<CartShopItem>();
  }
}
