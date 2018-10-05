using System.Collections.Generic;

namespace DemoProject.DLL.Models
{
  public class MenuItem : BaseEntity
  {
    public int Order { get; set; }
    public string Text { get; set; }
    public string IconPath { get; set; }

    public ICollection<ShopItem> Items { get; set; } = new List<ShopItem>();
  }
}
