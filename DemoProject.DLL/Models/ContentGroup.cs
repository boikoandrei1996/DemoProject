using System.Collections.Generic;

namespace DemoProject.DLL.Models
{
  public class ContentGroup : BaseEntity
  {
    public string Title { get; set; }
    public int Order { get; set; }
    public GroupName GroupName { get; set; }
    public ICollection<InfoObject> Items { get; set; } = new List<InfoObject>();
  }

  /// <summary>
  /// Name of content group
  /// </summary>
  public enum GroupName : int
  {
    /// <summary>
    /// Discount
    /// </summary>
    Discount = 1,

    /// <summary>
    /// Delivery
    /// </summary>
    Delivery = 2,

    /// <summary>
    /// About Us
    /// </summary>
    AboutUs = 3
  }
}
