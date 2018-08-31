namespace DemoProject.DLL.Infrastructure
{
  public class ServiceError
  {
    public ServiceError()
    {
      Code = string.Empty;
      Description = string.Empty;
    }

    public string Code { get; set; }
    public string Description { get; set; }
  }
}
