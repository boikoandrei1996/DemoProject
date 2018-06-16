using System.Collections.Generic;
using DemoProject.DLL.Infrastructure;
using Newtonsoft.Json;

namespace DemoProject.DLL.Models
{
  public class MenuItem : BaseEntity
  {
    public MenuItem()
    {
      Items = new List<ShopItem>();
    }

    public string Text { get; set; }

    [JsonConverter(typeof(ImageJsonConverter))]
    public byte[] Icon { get; set; }

    public string IconContentType { get; set; }

    public ICollection<ShopItem> Items { get; set; }
  }
}
