using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.OrderApiModels
{
  public class OrderEditModel
  {
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(20)]
    public string Mobile { get; set; }

    [Required]
    public string Address { get; set; }

    public string Comment { get; set; }

    public static Order Map(OrderEditModel model, Guid id)
    {
      if (model == null)
      {
        return null;
      }

      return new Order
      {
        Id = id,
        Name = model.Name,
        Mobile = model.Mobile,
        Address = model.Address,
        Comment = model.Comment
      };
    }
  }
}
