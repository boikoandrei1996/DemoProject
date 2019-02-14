using System;

namespace DemoProject.DAL.Models
{
  public class ChangeHistory : BaseEntity
  {
    public TableName Table { get; set; }
    public ActionType Action { get; set; }
    public DateTime TimeOfChange { get; private set; }

    public static ChangeHistory Create(TableName table, ActionType action)
    {
      return new ChangeHistory
      {
        Table = table,
        Action = action,
        TimeOfChange = DateTime.UtcNow
      };
    }

    public static TableName GetTableNameByGroupName(GroupName groupName)
    {
      switch (groupName)
      {
        case GroupName.Discount:
          return TableName.Discount;
        case GroupName.Delivery:
          return TableName.Delivery;
        case GroupName.AboutUs:
          return TableName.AboutUs;
        default:
          throw new NotImplementedException(nameof(GroupName));
      }
    }
  }

  public enum ActionType : short
  {
    Add = 0,
    Modify = 1,
    Delete = 2
  }

  public enum TableName : short
  {
    Discount = 0,
    Delivery = 1,
    AboutUs = 2,
    InfoObject = 3,
    MenuItem = 4
  }
}
