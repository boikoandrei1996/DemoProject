using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.DAL.Models
{
  public class AppUser : BaseEntity
  {
    public string Username { get; set; }

    public string Role { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfCreation { get; private set; } = DateTime.UtcNow;

    public DateTime? LastModified { get; set; }

    [InverseProperty("ApproveUser")]
    public ICollection<Order> ApprovedOrders { get; set; } = new List<Order>();

    [InverseProperty("RejectUser")]
    public ICollection<Order> RejectedOrders { get; set; } = new List<Order>();

    [InverseProperty("CloseUser")]
    public ICollection<Order> ClosedOrders { get; set; } = new List<Order>();
  }
}
