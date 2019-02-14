using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Extensions
{
  public static class EnumExtensions
  {
    public static string ToCustomString(this TableName action)
    {
      switch (action)
      {
        case TableName.Discount:
          return "Discount";
        case TableName.Delivery:
          return "Delivery";
        case TableName.AboutUs:
          return "AboutUs";
        case TableName.InfoObject:
          return "InfoObject";
        case TableName.MenuItem:
          return "MenuItem";
        default:
          return "Undefined";
      }
    }

    public static string ToCustomString(this ActionType action)
    {
      switch (action)
      {
        case ActionType.Add:
          return "Added";
        case ActionType.Modify:
          return "Modified";
        case ActionType.Delete:
          return "Deleted";
        default:
          return "Undefined";
      }
    }
  }
}
