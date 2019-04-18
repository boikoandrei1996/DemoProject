using System.ComponentModel.DataAnnotations;

namespace DemoProject.Shared.Models
{
  public abstract class BaseEntity<TId>
  {
    [Key]
    public TId Id { get; set; }
  }
}
