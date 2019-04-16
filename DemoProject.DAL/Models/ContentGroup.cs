using System;
using System.Collections.Generic;
using DemoProject.DAL.Enums;
using DemoProject.Shared.Models;

namespace DemoProject.DAL.Models
{
  public class ContentGroup : BaseEntity<Guid>
  {
    public string Title { get; set; }

    public int Order { get; set; }

    public ContentGroupName GroupName { get; set; }

    public DateTime DateOfCreation { get; private set; } = DateTime.UtcNow;

    public ICollection<InfoObject> Items { get; set; } = new List<InfoObject>();
  }
}
