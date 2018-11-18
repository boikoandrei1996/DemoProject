using System.Collections.Generic;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.PageModels
{
  public class OrderPage : IPage<Order>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<Order> Records { get; set; } = new List<Order>();
  }
}
