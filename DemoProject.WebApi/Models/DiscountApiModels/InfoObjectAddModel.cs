using System;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class InfoObjectAddModel
  {
    public string Content { get; set; }
    public string Type { get; set; }

    public static InfoObject Map(InfoObjectAddModel model)
    {
      return new InfoObject
      {
        Content = model.Content,
        Type = Enum.Parse<InfoObjectType>(model.Type, ignoreCase: true)
      };
    }
  }
}
