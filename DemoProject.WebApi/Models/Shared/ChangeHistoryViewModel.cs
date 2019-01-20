using System;
using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.Shared
{
  public class ChangeHistoryViewModel
  {
    public DateTime TimeOfChange { get; set; }

    public static ChangeHistoryViewModel Map(ChangeHistory model)
    {
      if (model == null)
      {
        return null;
      }

      return new ChangeHistoryViewModel
      {
        TimeOfChange = model.TimeOfChange
      };
    }
  }
}
