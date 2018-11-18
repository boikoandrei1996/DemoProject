using System.Collections.Generic;

namespace DemoProject.DAL.Models.Pages
{
  public class MenuItemPage : IPage<MenuItem>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<MenuItem> Records { get; set; } = new List<MenuItem>();
  }
}
