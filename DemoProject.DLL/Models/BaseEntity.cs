using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.DLL.Models
{
  public abstract class BaseEntity
  {
    [Key]
    public Guid Id { get; set; }
  }
}
