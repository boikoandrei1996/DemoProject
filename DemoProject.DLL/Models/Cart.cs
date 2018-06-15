using System.Collections.Generic;

namespace DemoProject.DLL.Models
{
  public class Cart : BaseEntity
  {
    public Cart()
    {
      CartShopItems = new List<CartShopItem>();
    }
    
    public ICollection<CartShopItem> CartShopItems { get; set; }
  }
}
