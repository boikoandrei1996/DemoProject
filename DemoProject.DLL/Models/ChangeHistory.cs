using System;

namespace DemoProject.DAL.Models
{
  public class ChangeHistory : BaseEntity
  {
    public string TableName { get; set; }
    public DateTime TimeOfChange { get; set; }

    public static ChangeHistory Create(string tableName)
    {
      return new ChangeHistory
      {
        TableName = tableName,
        TimeOfChange = DateTime.UtcNow
      };
    }

    public static string GetTableNameByGroupName(GroupName groupName)
    {
      switch (groupName)
      {
        case GroupName.Discount:
          return TableNames.Discount;
        case GroupName.Delivery:
          return TableNames.Delivery;
        case GroupName.AboutUs:
          return TableNames.AboutUs;
        default:
          throw new NotImplementedException(nameof(GroupName));
      }
    }
  }

  public static class TableNames
  {
    public static readonly string Discount = "Discount";
    public static readonly string Delivery = "Delivery";
    public static readonly string AboutUs = "AboutUs";
    public static readonly string InfoObject = "InfoObject";
    public static readonly string MenuItem = "MenuItem";
  }
}
