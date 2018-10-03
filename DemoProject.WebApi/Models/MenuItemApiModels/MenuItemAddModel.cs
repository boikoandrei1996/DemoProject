using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.MenuItemApiModels
{
  public class MenuItemAddModel
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

    public static MenuItem Map(MenuItemAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new MenuItem
      {
        Order = model.Order,
        Text = model.Text,
        Icon = new byte[0],
        IconContentType = model.IconContentType
      };
    }
  }
}
