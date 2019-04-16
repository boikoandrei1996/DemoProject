using System.ComponentModel.DataAnnotations;

namespace DemoProject.Shared.Models
{
  public abstract class BaseEntity<T>
  {
    [Key]
    public T Id { get; set; }
  }
}
