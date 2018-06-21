using System.Collections.Generic;
using System.Linq;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class DiscountAddModel
  {
    public string Title { get; set; }
    public ICollection<InfoObjectAddModel> Items { get; set; } = new List<InfoObjectAddModel>();

    public static Discount Map(DiscountAddModel model)
    {
      return new Discount
      {
        Title = model.Title,
        Items = model.Items.Select(x => InfoObjectAddModel.Map(x)).ToList()
      };
    }
  }
}
