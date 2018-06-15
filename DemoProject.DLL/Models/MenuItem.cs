using System.Collections.Generic;

namespace DemoProject.DLL.Models
{
  public class MenuItem : BaseEntity
  {
    public MenuItem()
    {
      Items = new List<ShopItem>();
    }

    public string Text { get; set; }
    
    public byte[] Icon { get; set; }

    public string IconContentType { get; set; }

    public ICollection<ShopItem> Items { get; set; }
  }
}
