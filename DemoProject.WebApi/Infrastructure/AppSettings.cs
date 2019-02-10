namespace DemoProject.WebApi.Infrastructure
{
  public sealed class AppSettings
  {
    public bool DatabaseRestore { get; set; }

    public string Secret { get; set; }
  }
}
