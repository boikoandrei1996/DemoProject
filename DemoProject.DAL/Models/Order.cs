using System;
using DemoProject.Shared.Models;

namespace DemoProject.DAL.Models
{
  public sealed class Order : BaseEntity<Guid>
  {
    public string Name { get; set; }

    public string Mobile { get; set; }

    public string Address { get; set; }

    public string Comment { get; set; }

    public DateTime DateOfCreation { get; private set; } = DateTime.UtcNow;

    public Guid CartId { get; set; }

    public Cart Cart { get; set; }

    // Approve
    public Guid? ApproveUserId { get; set; }

    public AppUser ApproveUser { get; set; }

    public DateTime? DateOfApproved { get; set; }

    // Reject
    public Guid? RejectUserId { get; set; }

    public AppUser RejectUser { get; set; }

    public DateTime? DateOfRejected { get; set; }

    // Close
    public Guid? CloseUserId { get; set; }

    public AppUser CloseUser { get; set; }

    public DateTime? DateOfClosed { get; set; }
  }
}
