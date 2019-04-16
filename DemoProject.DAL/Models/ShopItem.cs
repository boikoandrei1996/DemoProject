using System;
using System.Collections.Generic;
using DemoProject.Shared.Models;

namespace DemoProject.DAL.Models
{
  public class ShopItem : BaseEntity<Guid>
  {
    public string Title { get; set; }

    public string Description { get; set; }

    public string ImagePath { get; set; }

    public Guid MenuItemId { get; set; }

    public MenuItem MenuItem { get; set; }

    public ICollection<ShopItemDetail> Details { get; set; } = new List<ShopItemDetail>();
  }
}
