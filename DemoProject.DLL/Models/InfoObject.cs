using System;

namespace DemoProject.DLL.Models
{
  public class InfoObject : BaseEntity
  {
    public string Content { get; set; }

    public InfoObjectType Type { get; set; }
    
    public Guid DiscountId { get; set; }
    public Discount Discount { get; set; }
  }

  public enum InfoObjectType : int
  {
    HTML,
    Text,
    Image
  }
}
