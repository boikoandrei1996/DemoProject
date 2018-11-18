using System.Collections.Generic;

namespace DemoProject.DAL.Models.Pages
{
  public class ContentGroupPage : IPage<ContentGroup>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<ContentGroup> Records { get; set; } = new List<ContentGroup>();
  }
}
