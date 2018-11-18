using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DAL.Models;

namespace DemoProject.WebApi.Models.CartApiModels
{
  public class CartAddModel
  {
    [Required]
    public Guid Id { get; set; }

    public static Cart Map(CartAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new Cart { Id = model.Id };
    }
  }
}
