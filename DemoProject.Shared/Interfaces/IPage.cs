using System.Collections.Generic;

namespace DemoProject.Shared.Interfaces
{
  public interface IPage<T>
  {
    int CurrentPage { get; set; }
    int PageSize { get; set; }
    int TotalPages { get; set; }
    ICollection<T> Records { get; set; }
  }
}
