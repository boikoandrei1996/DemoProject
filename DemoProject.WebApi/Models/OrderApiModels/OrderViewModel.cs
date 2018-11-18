using System;
using DemoProject.DAL.Models;
using DemoProject.WebApi.Models.CartApiModels;

namespace DemoProject.WebApi.Models.OrderApiModels
{
  public class OrderViewModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public string Comment { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime? DateOfApproved { get; set; }
    public DateTime? DateOfRejected { get; set; }
    public DateTime? DateOfClosed { get; set; }
    public CartViewModel Cart { get; set; }

    public static OrderViewModel Map(Order model)
    {
      if (model == null)
      {
        return null;
      }

      return new OrderViewModel
      {
        Id = model.Id,
        Name = model.Name,
        Mobile = model.Mobile,
        Address = model.Address,
        Comment = model.Comment,
        DateOfCreation = model.DateOfCreation,
        DateOfApproved = model.DateOfApproved,
        DateOfRejected = model.DateOfRejected,
        DateOfClosed = model.DateOfClosed,
        Cart = CartViewModel.Map(model.Cart)
      };
    }
  }
}
