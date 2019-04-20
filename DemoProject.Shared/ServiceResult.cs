using System.Collections.Generic;
using System.Threading.Tasks;

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
    internal ServiceResult(ServiceResultKey key)
    {
      this.Key = key;
    }

    internal ServiceResult(ServiceResultKey key, ServiceError error) : this(key)
    {
      if (error != null)
      {
        this.Errors.Add(error);
      }
    }

    internal ServiceResult(ServiceResultKey key, object model) : this(key)
    {
      this.Model = model;
    }

    public ServiceResultKey Key { get; }

    public ICollection<ServiceError> Errors { get; } = new List<ServiceError>();

    public object Model { get; private set; }

    public object ViewModel { get; set; }

    public bool IsSuccess
    {
      get
      {
        return this.Key == ServiceResultKey.Success;
      }
    }

    public bool HasModel
    {
      get
      {
        return this.Model != null;
      }
    }

    public void SetModelIfSuccess<TModel>(TModel model)
    {
      if (this.IsSuccess)
      {
        this.Model = model;
      }
    }

    public async Task SetModelIfSuccessAsync<TModel>(Task<TModel> taskModel)
    {
      if (this.IsSuccess)
      {
        this.Model = await taskModel;
      }
    }

    public bool TryCastModel<TModel>(out TModel model)
      where TModel : class
    {
      if (this.HasModel && this.Model is TModel)
      {
        model = this.Model as TModel;
        return true;
      }
      else
      {
        model = null;
        return false;
      }
    }
  }
}
