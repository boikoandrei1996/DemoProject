using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.DAL.Models
{
  public abstract class BaseEntity
  {
    [Key]
    public Guid Id { get; set; }
  }
}
