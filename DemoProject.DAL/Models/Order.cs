using System;

namespace DemoProject.DAL.Models
{
  public class Order : BaseEntity
  {
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public string Comment { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime? DateOfApproved { get; set; }
    public DateTime? DateOfRejected { get; set; }
    public DateTime? DateOfClosed { get; set; }

    public Guid CartId { get; set; }
    public Cart Cart { get; set; }
  }
}
