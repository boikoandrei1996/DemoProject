using System;

namespace DemoProject.DAL.Models
{
  public class InfoObject : BaseEntity
  {
    public string Content { get; set; }
    public InfoObjectType Type { get; set; }
    public int SubOrder { get; set; }
    public Guid ContentGroupId { get; set; }
    public ContentGroup ContentGroup { get; set; }
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
