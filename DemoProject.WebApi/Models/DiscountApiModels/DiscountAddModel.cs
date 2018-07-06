using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Models.InfoObjectApiModels;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class DiscountAddModel
  {
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    public ICollection<InfoObjectAddModel> Items { get; set; } = new List<InfoObjectAddModel>();

    public static Discount Map(DiscountAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new Discount
      {
        Title = model.Title,
        Items = model.Items.Select(x => InfoObjectAddModel.Map(x)).ToList()
      };
    }
  }
}
