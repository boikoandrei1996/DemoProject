using System;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.WebApi.Extensions;

namespace DemoProject.WebApi.Models.InfoObjectApiModels
{
  public class InfoObjectViewModel
  {
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Type { get; set; }
    public int SubOrder { get; set; }
    public Guid ContentGroupId { get; set; }
    public DateTime DateOfCreation { get; set; }

    public static InfoObjectViewModel Map(InfoObject model)
    {
      if (model == null)
      {
        return null;
      }

      var content = model.Content;
      if (model.Type == InfoObjectType.Image)
      {
        content = Constants.GetFullPathToImage(model.Content);
      }

      return new InfoObjectViewModel
      {
        Id = model.Id,
        Content = content,
        Type = model.Type.ToCustomString(),
        SubOrder = model.SubOrder,
        ContentGroupId = model.ContentGroupId,
        DateOfCreation = model.DateOfCreation
      };
    }
  }
}
