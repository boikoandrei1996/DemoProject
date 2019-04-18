using System;
using System.Collections.Generic;

namespace DemoProject.Shared
{
  public sealed class Page<TModel>
  {
    public int Current { get; set; }
    public int Size { get; set; }
    public int Total { get; set; }
    public ICollection<TModel> Records { get; set; } = new List<TModel>();

    public static Page<TModel> Create(int pageSize, int pageIndex, int totalItemsCount)
    {
      Check.Positive(pageSize, nameof(pageSize));
      Check.Positive(pageIndex, nameof(pageIndex));
      Check.PositiveOr0(totalItemsCount, nameof(totalItemsCount));

      if (totalItemsCount == 0)
      {
        return new Page<TModel>
        {
          Size = pageSize,
          Current = 1,
          Total = 1
        };
      }

      var totalPages = (int)Math.Ceiling(totalItemsCount / (decimal)pageSize);
      var currentPage = pageIndex > totalPages ? totalPages : pageIndex;

      return new Page<TModel>
      {
        Size = pageSize,
        Current = currentPage,
        Total = totalPages
      };
    }
  }
}
