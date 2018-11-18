using System.Collections.Generic;

namespace DemoProject.DAL.Models.Pages
{
  public interface IPage<T>
  {
    int CurrentPage { get; set; }
    int PageSize { get; set; }
    int TotalPages { get; set; }
    ICollection<T> Records { get; set; }
  }
}
