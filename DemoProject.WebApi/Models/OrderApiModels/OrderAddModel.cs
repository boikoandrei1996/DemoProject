using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.OrderApiModels
{
  public class OrderAddModel
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

    [Required]
    public Guid CartId { get; set; }

    public static Order Map(OrderAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new Order
      {
        Name = model.Name,
        Mobile = model.Mobile,
        Address = model.Address,
        Comment = model.Comment,
        CartId = model.CartId
      };
    }
  }
}
