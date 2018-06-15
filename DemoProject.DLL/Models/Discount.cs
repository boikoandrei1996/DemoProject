using System.Collections.Generic;

namespace DemoProject.DLL.Models
{
  public class Discount : BaseEntity
  {
    public Discount()
    {
      Items = new List<InfoObject>();
    }

    public string Title { get; set; }

    public ICollection<InfoObject> Items { get; set; }
  }
}
