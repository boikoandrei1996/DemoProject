using System;
using System.Collections.Generic;

namespace DemoProject.Shared
{
  public enum ServiceResultKey : int
  {
    Success = 0,
    BadRequest = 1,
    NotFound = 2,
    InternalServerError = 3,
    ModelCreated = 4,
    ModelUpdated = 5
  }

  public class ServiceResult
  {
    public ServiceResult(ServiceResultKey key) : this(key, error: null) { }

    public ServiceResult(ServiceResultKey key, ServiceError error)
    {
      this.Key = key;

      if (error != null)
      {
        this.Errors.Add(error);
      }
    }

    public ServiceResult(ServiceResultKey key, Guid modelId)
    {
      this.Key = key;
      this.ModelId = modelId;
    }

    public ServiceResult(ServiceResultKey key, object model)
    {
      this.Key = key;
      this.Model = model;
    }

    public ServiceResultKey Key { get; }

    public ICollection<ServiceError> Errors { get; } = new List<ServiceError>();

    public Guid ModelId { get; }

    public object Model { get; set; }
  }
}
