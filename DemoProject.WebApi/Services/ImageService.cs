using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DemoProject.WebApi.Services
{
  public class ImageService
  {
    private readonly IHostingEnvironment _environment;

    public ImageService(
      IHostingEnvironment environment)
    {
      _environment = environment;
    }

    public async Task<ServiceResult> SaveAsync(IFormFile file, string path)
    {
      Check.NotNull(file, nameof(file));
      Check.NotNullOrEmpty(path, nameof(path));

      if (file.Length <= 0)
      {
        return ServiceResultFactory.BadRequestResult(nameof(file), "File not found.");
      }

      var relativePath = Constants.GetRelativePathToImage(path);
      var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

      if (File.Exists(fullPath))
      {
        return ServiceResultFactory.BadRequestResult(nameof(path), "File already exist.");
      }

      var dirPath = Path.GetDirectoryName(fullPath);
      if (Directory.Exists(dirPath) == false)
      {
        Directory.CreateDirectory(dirPath);
      }

      using (var fileStream = new FileStream(fullPath, FileMode.Create))
      {
        await file.CopyToAsync(fileStream);
      }

      return ServiceResultFactory.Success;
    }

    public IEnumerable<string> GetImages(string searchPattern = null)
    {
      if (string.IsNullOrEmpty(searchPattern))
      {
        searchPattern = "*.*";
      }

      var root = Path.Combine(_environment.WebRootPath, Constants.DEFAULT_PATH_TO_IMAGE);

      var files = Directory.GetFiles(root, searchPattern, SearchOption.AllDirectories);

      return files.Select(x => Path.GetRelativePath(root, x));
    }

    public ServiceResult Delete(string path)
    {
      Check.NotNullOrEmpty(path, nameof(path));

      var relativePath = Constants.GetRelativePathToImage(path);
      var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

      if (File.Exists(fullPath) == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(path), "File not found.");
      }

      File.Delete(fullPath);

      return ServiceResultFactory.Success;
    }
  }
}
