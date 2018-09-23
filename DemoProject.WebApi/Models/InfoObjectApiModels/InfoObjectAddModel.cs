using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Attributes;

namespace DemoProject.WebApi.Models.InfoObjectApiModels
{
  public class InfoObjectAddModel
  {
    [Required]
    public string Content { get; set; }

    [Required]
    [EnumRangeValidation]
    public string Type { get; set; }

    [Required]
    public int SubOrder { get; set; }

    public Guid DiscountId { get; set; }

    public static InfoObject Map(InfoObjectAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new InfoObject
      {
        Content = model.Content,
        Type = Enum.Parse<InfoObjectType>(model.Type, ignoreCase: true),
        SubOrder = model.SubOrder,
        DiscountId = model.DiscountId
      };
    }
  }
}
