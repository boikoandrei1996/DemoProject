using System;
using DemoProject.DLL.Models;

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

      return new InfoObjectViewModel
      {
        Id = model.Id,
        ContentGroupId = model.ContentGroupId,
        Content = model.Content,
        SubOrder = model.SubOrder,
        Type = model.Type.ToString()
      };
    }
  }
}
