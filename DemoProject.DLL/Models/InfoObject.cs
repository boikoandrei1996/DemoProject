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

  /// <summary>
  /// Type of InfoObject
  /// </summary>
  public enum InfoObjectType : int
  {
    /// <summary>
    /// HTML
    /// </summary>
    HTML = 1,

    /// <summary>
    /// Text
    /// </summary>
    Text = 2,

    /// <summary>
    /// Image (not used)
    /// </summary>
    Image = 3
  }
}
