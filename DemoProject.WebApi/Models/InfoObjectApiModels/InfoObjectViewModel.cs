using System;
using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.InfoObjectApiModels
{
  public class InfoObjectViewModel
  {
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Type { get; set; }
    public int SubOrder { get; set; }
    public Guid ContentGroupId { get; set; }

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
        Type = model.Type.ToString(),
        SubOrder = model.SubOrder,
        ContentGroupId = model.ContentGroupId,
      };
    }
  }
}
