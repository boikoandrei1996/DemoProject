using System;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class InfoObjectAddModel
  {
    public Guid DiscountId { get; set; }
    public string Content { get; set; }
    public string Type { get; set; }

    public static InfoObject Map(InfoObjectAddModel model)
    {
      return new InfoObject
      {
        DiscountId = model.DiscountId,
        Content = model.Content,
        Type = Enum.Parse<InfoObjectType>(model.Type, ignoreCase: true)
      };
    }
  }
}
