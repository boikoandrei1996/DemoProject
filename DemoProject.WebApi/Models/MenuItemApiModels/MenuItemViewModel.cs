using System;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.MenuItemApiModels
{
  public class MenuItemViewModel
  {
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Text { get; set; }
    public string IconPath { get; set; }
    public string IconContentType { get; set; }

    public static MenuItemViewModel Map(MenuItem model)
    {
      if (model == null)
      {
        return null;
      }

      return new MenuItemViewModel
      {
        Id = model.Id,
        Order = model.Order,
        Text = model.Text,
        IconPath = model.IconPath,
        IconContentType = model.IconContentType
      };
    }
  }
}
