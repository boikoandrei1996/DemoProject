using System;
using DemoProject.DAL.Models;
using DemoProject.WebApi.Extensions;

namespace DemoProject.WebApi.Models.Shared
{
  public class ChangeHistoryViewModel
  {
    public string TableName { get; set; }
    public string ActionType { get; set; }
    public DateTime TimeOfChange { get; set; }

    public static ChangeHistoryViewModel Map(ChangeHistory model)
    {
      if (model == null)
      {
        return null;
      }

      return new ChangeHistoryViewModel
      {
        TableName = model.Table.ToCustomString(),
        ActionType = model.Action.ToCustomString(),
        TimeOfChange = model.TimeOfChange
      };
    }
  }
}
