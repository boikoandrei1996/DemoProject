using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared.Attributes;
using DemoProject.Shared.Extensions;

namespace DemoProject.WebApi.Models.InfoObjectApiModels
{
  public class InfoObjectEditModel
  {
    public string Content { get; set; }

    // [EnumRangeValidation(typeof(InfoObjectType))]
    public string Type { get; set; }

    public int SubOrder { get; set; }

    public Guid ContentGroupId { get; set; }

    public static InfoObject Map(InfoObjectEditModel model, Guid id)
    {
      if (model == null)
      {
        return null;
      }

      var result =  new InfoObject
      {
        Id = id,
        SubOrder = model.SubOrder,
        ContentGroupId = model.ContentGroupId
      };

      if (model.Type.IsNotNullOrEmpty())
      {
        var type = Enum.Parse<InfoObjectType>(model.Type, ignoreCase: true);

        var content = model.Content;
        if (type == InfoObjectType.Image)
        {
          content = Constants.GetRelativePathToImage(model.Content);
        }

        result.Type = type;
        result.Content = content;
      }

      return result;
    }
  }
}
