namespace DemoProject.WebApi
{
  public static class Constants
  {
    // Should be const, because of used as attribute argument
    public const string WEB_CONTENT_ROOT_PATH = "wwwroot/";
    public const string DEFAULT_PATH_TO_IMAGE = "images/";

    public static readonly int DEFAULT_PAGE_INDEX = 1;
    public static readonly int DEFAULT_PAGE_SIZE = 2;

    private static readonly string BASE_URL = "https://demoprojectapi.azurewebsites.net/";

    public static string GetRelativePathToImage(string filename)
    {
      return Constants.DEFAULT_PATH_TO_IMAGE + filename;
    }

    public static string GetFullPathToImage(string relativePath)
    {
      return Constants.BASE_URL + relativePath;
    }
  }
}
