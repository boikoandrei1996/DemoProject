using System.Collections.Generic;
using DemoProject.DLL.Models;

namespace DemoProject.DLL.Pages
{
  public class DiscountPage
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<Discount> Records { get; set; } = new List<Discount>();
  }
}
