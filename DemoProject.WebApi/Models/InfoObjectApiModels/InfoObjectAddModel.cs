using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Models;
using DemoProject.Shared.Attributes;

namespace DemoProject.WebApi.Models.InfoObjectApiModels
{
  public class InfoObjectAddModel
  {
    [Required]
    public string Content { get; set; }

    [Required]
    [EnumRangeValidation(typeof(InfoObjectType))]
    public string Type { get; set; }

    [Required]
    [MinimumValueValidation]
    public int SubOrder { get; set; }

    [Required]
    public Guid ContentGroupId { get; set; }

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
        ContentGroupId = model.ContentGroupId
      };
    }
  }
}
