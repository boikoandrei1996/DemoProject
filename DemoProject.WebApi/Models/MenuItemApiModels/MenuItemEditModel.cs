using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.MenuItemApiModels
{
  public class MenuItemEditModel
  {
    [Required]
    public int Order { get; set; }

    [Required]
    [MaxLength(100)]
    public string Text { get; set; }

    // public byte[] Icon { get; set; }

    [Required]
    [MaxLength(20)]
    public string IconContentType { get; set; }

    public static MenuItem Map(MenuItemEditModel model, Guid id)
    {
      if (model == null)
      {
        return null;
      }

      return new MenuItem
      {
        Id = id,
        Order = model.Order,
        Text = model.Text,
        IconPath = string.Empty,
        IconContentType = model.IconContentType
      };
    }
  }
}
