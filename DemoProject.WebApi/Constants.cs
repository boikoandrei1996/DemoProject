namespace DemoProject.WebApi
{
  public sealed class Constants
  {
    public const int DEFAULT_PAGE_INDEX = 1;
    public const int DEFAULT_PAGE_SIZE = 2;
    public const string DEFAULT_PATH_TO_IMAGE = "Images/";
    private const string BASE_URL = "https://demoprojectapi.azurewebsites.net/";

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
