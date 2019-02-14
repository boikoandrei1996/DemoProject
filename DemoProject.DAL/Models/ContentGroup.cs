using System;
using System.Collections.Generic;
using DemoProject.DAL.Enums;

namespace DemoProject.DAL.Models
{
  public class ContentGroup : BaseEntity
  {
    public string Title { get; set; }

    public int Order { get; set; }

    public ContentGroupName GroupName { get; set; }

    public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

    public ICollection<InfoObject> Items { get; set; } = new List<InfoObject>();
  }
}
