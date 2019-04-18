using System.Collections.Generic;

namespace DemoProject.Shared
{
  public enum ServiceResultKey : int
  {
    Success = 0,
    BadRequest = 1,
    NotFound = 2,
    InternalServerError = 3
  }

  public sealed class ServiceResult
  {
    internal ServiceResult(ServiceResultKey key, object model = null)
    {
      this.Key = key;
      this.Model = model;
    }

    internal ServiceResult(ServiceResultKey key, ServiceError error)
    {
      this.Key = key;

      if (error != null)
      {
        this.Errors.Add(error);
      }
    }

    public ServiceResultKey Key { get; }

    public ICollection<ServiceError> Errors { get; } = new List<ServiceError>();

    public object Model { get; }
  }
}
