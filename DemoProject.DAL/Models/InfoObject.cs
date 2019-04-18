using System;
using DemoProject.DAL.Enums;
using DemoProject.Shared.Models;

namespace DemoProject.DAL.Models
{
  public sealed class InfoObject : BaseEntity<Guid>
  {
    public string Content { get; set; }

    public InfoObjectType Type { get; set; }

    public int SubOrder { get; set; }

    public DateTime DateOfCreation { get; private set; } = DateTime.UtcNow;

    public Guid ContentGroupId { get; set; }

    public ContentGroup ContentGroup { get; set; }
  }
}
