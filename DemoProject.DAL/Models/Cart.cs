using System;
using System.Collections.Generic;
using DemoProject.Shared.Models;

namespace DemoProject.DAL.Models
{
  public sealed class Cart : BaseEntity<Guid>
  {
    public DateTime DateOfCreation { get; private set; } = DateTime.UtcNow;

    public ICollection<CartShopItem> CartShopItems { get; set; } = new List<CartShopItem>();
  }
}
