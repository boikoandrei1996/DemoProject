using System;
using System.Collections.Generic;

namespace DemoProject.DAL.Models
{
  public class Cart : BaseEntity
  {
    public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

    public ICollection<CartShopItem> CartShopItems { get; set; } = new List<CartShopItem>();
  }
}
