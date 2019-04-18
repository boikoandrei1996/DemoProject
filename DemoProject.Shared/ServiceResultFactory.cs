namespace DemoProject.Shared
{
  public static class ServiceResultFactory
  {
    public static ServiceResult Success
    {
      get { return new ServiceResult(ServiceResultKey.Success); }
    }

    public static ServiceResult NotFound
    {
      get { return new ServiceResult(ServiceResultKey.NotFound); }
    }

    public static ServiceResult InternalServerError
    {
      get { return new ServiceResult(ServiceResultKey.InternalServerError); }
    }

    public static ServiceResult SuccessResult<TModel>(TModel model)
    {
      return new ServiceResult(ServiceResultKey.Success, model);
    }

    public static ServiceResult BadRequestResult(string code, string description)
    {
      return new ServiceResult(ServiceResultKey.BadRequest, new ServiceError
      {
        Code = code,
        Description = description
      });
    }

    public static ServiceResult InternalServerErrorResult(string message)
    {
      return new ServiceResult(ServiceResultKey.InternalServerError, new ServiceError
      {
        Description = message
      });
    }
  }
}
