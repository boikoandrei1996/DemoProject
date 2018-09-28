using System.Collections.Generic;

namespace DemoProject.DLL.Models.Pages
{
  public class ContentGroupPage
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<ContentGroup> Records { get; set; } = new List<ContentGroup>();
  }
}
