using System;
using DemoProject.DAL.Enums;

namespace DemoProject.DAL.Models
{
  public class ChangeHistory : BaseEntity
  {
    public TableName Table { get; set; }

    public ActionType Action { get; set; }

    public DateTime TimeOfChange { get; private set; } = DateTime.UtcNow;

    public static ChangeHistory Create(TableName table, ActionType action)
    {
      return new ChangeHistory
      {
        Table = table,
        Action = action
      };
    }

    public static ChangeHistory Create(ContentGroupName groupName, ActionType action)
    {
      TableName table;
      switch (groupName)
      {
        case ContentGroupName.Discount:
          table = TableName.Discount;
          break;
        case ContentGroupName.Delivery:
          table = TableName.Delivery;
          break;
        case ContentGroupName.AboutUs:
          table = TableName.AboutUs;
          break;
        default:
          throw new NotImplementedException(nameof(ContentGroupName));
      }

      return new ChangeHistory
      {
        Table = table,
        Action = action
      };
    }
  }
}
