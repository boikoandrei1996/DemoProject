using System;
using System.Collections.Generic;

namespace DemoProject.DLL.Infrastructure
{
  public class ServiceResult
  {
    public ServiceResult(ServiceResultKey key) : this(key, null) { }

    public ServiceResult(ServiceResultKey key, ServiceError error)
    {
      Key = key;
      Errors = new List<ServiceError>();

      if (error != null)
      {
        Errors.Add(error);
      }
    }

    public ServiceResult(ServiceResultKey key, Guid modelId)
    {
      Key = key;
      ModelId = modelId;
    }

    public ServiceResult(ServiceResultKey key, object model)
    {
      Key = key;
      Model = model;
    }

    public ServiceResultKey Key { get; }

    public ICollection<ServiceError> Errors { get; }

    public Guid ModelId { get; }

    public object Model { get; }
  }

  public enum ServiceResultKey : int
  {
    Success = 0,
    BadRequest = 1,
    NotFound = 2,
    InternalServerError = 3,
    ModelCreated = 4,
    ModelUpdated = 5
  }
}
