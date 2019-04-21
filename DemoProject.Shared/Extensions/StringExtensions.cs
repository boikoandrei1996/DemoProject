namespace DemoProject.Shared.Extensions
{
  public static class StringExtensions
  {
    public static bool IsNotNullOrEmpty(this string value)
    {
      return string.IsNullOrEmpty(value) == false;
    }
  }
}
