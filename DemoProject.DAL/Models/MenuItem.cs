using System;
using System.Collections.Generic;
using DemoProject.Shared.Models;

namespace DemoProject.DAL.Models
{
  public sealed class MenuItem : BaseEntity<Guid>
  {
    public int Order { get; set; }

    public string Text { get; set; }

    public string IconPath { get; set; }

    public ICollection<ShopItem> Items { get; set; } = new List<ShopItem>();
  }
}
