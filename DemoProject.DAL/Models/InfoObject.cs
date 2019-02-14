using System;
using DemoProject.DAL.Enums;

namespace DemoProject.DAL.Models
{
  public class InfoObject : BaseEntity
  {
    public string Content { get; set; }

    public InfoObjectType Type { get; set; }

    public int SubOrder { get; set; }

    public DateTime DateOfCreation { get; private set; } = DateTime.UtcNow;

    public Guid ContentGroupId { get; set; }

    public ContentGroup ContentGroup { get; set; }
  }
}
