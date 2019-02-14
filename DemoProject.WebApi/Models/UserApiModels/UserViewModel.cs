using System;
using System.Collections.Generic;
using System.Linq;
using DemoProject.DAL.Models;
using DemoProject.WebApi.Models.OrderApiModels;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public class UserViewModel
  {
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime? LastModified { get; set; }
    public ICollection<OrderShortViewModel> ApprovedOrders { get; set; } = new List<OrderShortViewModel>();
    public ICollection<OrderShortViewModel> RejectedOrders { get; set; } = new List<OrderShortViewModel>();
    public ICollection<OrderShortViewModel> ClosedOrders { get; set; } = new List<OrderShortViewModel>();

    public static UserViewModel Map(AppUser model)
    {
      if (model == null)
      {
        return null;
      }

      return new UserViewModel
      {
        Id = model.Id,
        Username = model.Username,
        Role = model.Role,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        EmailConfirmed = model.EmailConfirmed,
        PhoneNumber = model.PhoneNumber,
        DateOfCreation = model.DateOfCreation,
        LastModified = model.LastModified,
        ApprovedOrders = model.ApprovedOrders.Select(OrderShortViewModel.Map).ToList(),
        RejectedOrders = model.RejectedOrders.Select(OrderShortViewModel.Map).ToList(),
        ClosedOrders = model.ClosedOrders.Select(OrderShortViewModel.Map).ToList()
      };
    }
  }
}
