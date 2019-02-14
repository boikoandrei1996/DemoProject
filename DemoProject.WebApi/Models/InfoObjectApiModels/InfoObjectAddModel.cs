using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Enums;
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

      var type = Enum.Parse<InfoObjectType>(model.Type, ignoreCase: true);

      var content = model.Content;
      if (type == InfoObjectType.Image)
      {
        content = Constants.GetRelativePathToImage(model.Content);
      }

      return new InfoObject
      {
        Content = content,
        Type = type,
        SubOrder = model.SubOrder,
        ContentGroupId = model.ContentGroupId
      };
    }
  }
}
