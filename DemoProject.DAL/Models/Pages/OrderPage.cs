using System.Collections.Generic;

namespace DemoProject.DAL.Models.Pages
{
  public class OrderPage : IPage<Order>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<Order> Records { get; set; } = new List<Order>();
  }
}
