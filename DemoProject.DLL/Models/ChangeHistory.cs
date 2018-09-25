using System;

namespace DemoProject.DLL.Models
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
  }

  public static class TableNames
  {
    public static readonly string Discounts = "Discounts";
    public static readonly string InfoObjects = "InfoObjects";
  }
}
