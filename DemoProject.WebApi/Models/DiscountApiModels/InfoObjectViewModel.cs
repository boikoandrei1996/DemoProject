using System;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class InfoObjectViewModel
  {
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Type { get; set; }
    public Guid DiscountId { get; set; }

    public static InfoObjectViewModel Map(InfoObject model)
    {
      return new InfoObjectViewModel
      {
        Id = model.Id,
        DiscountId = model.DiscountId,
        Content = model.Content,
        Type = model.Type.ToString()
      };
    }
  }
}
