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

    public static TableName GetTableNameByGroupName(ContentGroupName groupName)
    {
      switch (groupName)
      {
        case ContentGroupName.Discount:
          return TableName.Discount;
        case ContentGroupName.Delivery:
          return TableName.Delivery;
        case ContentGroupName.AboutUs:
          return TableName.AboutUs;
        default:
          throw new NotImplementedException(nameof(ContentGroupName));
      }
    }
  }
}
