using System.Collections.Generic;

namespace DemoProject.DAL.Models.Pages
{
  public class CartPage : IPage<Cart>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<Cart> Records { get; set; } = new List<Cart>();
  }
}
