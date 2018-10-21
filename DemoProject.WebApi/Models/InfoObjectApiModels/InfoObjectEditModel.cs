using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Attributes.ValidationAttributes;

namespace DemoProject.WebApi.Models.InfoObjectApiModels
{
  public class InfoObjectEditModel
  {
    [Required]
    public string Content { get; set; }

    [Required]
    [EnumRangeValidation]
    public string Type { get; set; }

    [Required]
    [MinimumValueValidation]
    public int SubOrder { get; set; }

    [Required]
    public Guid ContentGroupId { get; set; }

    public static InfoObject Map(InfoObjectEditModel model, Guid id)
    {
      if (model == null)
      {
        return null;
      }

      return new InfoObject
      {
        Id = id,
        ContentGroupId = model.ContentGroupId,
        Content = model.Content,
        SubOrder = model.SubOrder,
        Type = Enum.Parse<InfoObjectType>(model.Type, ignoreCase: true)
      };
    }
  }
}
