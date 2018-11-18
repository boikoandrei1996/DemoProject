using System.Collections.Generic;

namespace DemoProject.DAL.Models.Pages
{
  public class ShopItemPage : IPage<ShopItem>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<ShopItem> Records { get; set; } = new List<ShopItem>();
  }
}
