using System.Collections.Generic;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.PageModels
{
  public class ContentGroupPage : IPage<ContentGroup>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<ContentGroup> Records { get; set; } = new List<ContentGroup>();
  }
}
