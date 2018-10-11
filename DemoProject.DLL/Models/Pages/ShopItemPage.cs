using System.Collections.Generic;

namespace DemoProject.DLL.Models.Pages
{
  public class ShopItemPage
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<ShopItem> Records { get; set; } = new List<ShopItem>();
  }
}
